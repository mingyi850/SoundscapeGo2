using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using STT;



//Adapted from UnityForum user 'gregzo's code https://forum.unity.com/threads/writing-audiolistener-getoutputdata-to-wav-problem.119295/#post-806734


public class AudioConverterWav : MonoBehaviour{
	
	private int bufferSize;
	private int numBuffers;
	private int outputRate = 16000;
	private string fileName = "recTest.wav";
	private int headerSize = 44; //default for uncompressed wav
	private float[] sampleData;
	private float[] truncatedData;
	private bool recOutput = false;
	private AudioClip newRecording;
	private FileStream fileStream;
	private SpeechToTextHandler sttHandler;
	private FileStream fakeFile;
	private string fakeFileName = "fakeTest.wav";

	void Awake()
	{
		//AudioSettings.outputSampleRate = outputRate;
		//AudioSettings.GetConfiguration().sampleRate = outputRate;

	}

	void Start()
	{
		sttHandler = gameObject.GetComponent<SpeechToTextHandler> ();
		AudioSettings.GetDSPBufferSize(out bufferSize,out numBuffers);

	}

	void Update() {
		if (Input.GetKeyDown ("r")) {
			if (recOutput == false) {
				Debug.Log ("rec");
				newRecording = Microphone.Start (null, false, 10, outputRate);
				recOutput = true;
			} else {
				int lastTime = Microphone.GetPosition(null);
				Microphone.End (null);
				Debug.Log ("rec stop");
				sampleData = new float[newRecording.samples];
				//Used to later cut audio clip at last time
				truncatedData = new float[lastTime];
				newRecording.GetData (sampleData, 0);
				Array.Copy (sampleData, truncatedData, truncatedData.Length - 1);

				StartWriting (fileName);
				ConvertAndWrite (truncatedData);
				WriteHeader (); 
				StartCoroutine(sttHandler.GetSpeechToText(getFileStreamBytes()));
				recOutput = false;
				Debug.Log ("End of conversion");
			}
		}
	}

	private void StartWriting(string name)
	{
		fileStream = new FileStream(name, FileMode.Create);
		byte emptyByte = new byte();

		for(int i = 0; i<headerSize; i++) //preparing the header
		{
			fileStream.WriteByte(emptyByte);
		}
	}

	/*private void OnAudioFilterRead(float[] data, int channels )
	{
		if(recOutput)
		{
			ConvertAndWrite(data); //audio data is interlaced
		}
	}*/

	private void ConvertAndWrite(float[] dataSource)
	{

		Int16[] intData = new Int16[dataSource.Length];
		//converting in 2 steps : float[] to Int16[], //then Int16[] to Byte[]

		Byte[] bytesData = new Byte[dataSource.Length*2];
		//bytesData array is twice the size of
		//dataSource array because a float converted in Int16 is 2 bytes.

		int rescaleFactor = 32767; //to convert float to Int16

		for (int i = 0; i<dataSource.Length;i++)
		{
			intData[i] = (Int16)(dataSource[i]*rescaleFactor);
			Byte[] byteArr = new Byte[2];
			byteArr = BitConverter.GetBytes(intData[i]);
			byteArr.CopyTo(bytesData,i*2);
		}

		fileStream.Write(bytesData,0,bytesData.Length);
	}

	private void WriteHeader()
	{

		fileStream.Seek(0,SeekOrigin.Begin);

		Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(riff,0,4);

		Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length-8);
		fileStream.Write(chunkSize,0,4);

		Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(wave,0,4);

		Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(fmt,0,4);

		Byte[] subChunk1 = BitConverter.GetBytes(16);
		fileStream.Write(subChunk1,0,4);

		UInt16 two = 2;
		UInt16 one = 1;

		Byte[] audioFormat = BitConverter.GetBytes(one);
		fileStream.Write(audioFormat,0,2);

		Byte[] numChannels = BitConverter.GetBytes(one);
		fileStream.Write(numChannels,0,2);

		Byte[] sampleRate = BitConverter.GetBytes(outputRate);
		fileStream.Write(sampleRate,0,4);

		Byte[] byteRate = BitConverter.GetBytes(outputRate*2);
		// sampleRate * bytesPerSample*number of channels, here 44100*2*1

		fileStream.Write(byteRate,0,4);

		UInt16 four = 4;
		Byte[] blockAlign = BitConverter.GetBytes(two);
		fileStream.Write(blockAlign,0,2);

		//bitrate = 16
		UInt16 sixteen = 16;
		Byte[] bitsPerSample = BitConverter.GetBytes(sixteen);
		fileStream.Write(bitsPerSample,0,2);

		Byte[] dataString = System.Text.Encoding.UTF8.GetBytes("data");
		fileStream.Write(dataString,0,4);

		Byte[] subChunk2 = BitConverter.GetBytes(fileStream.Length-headerSize);
		fileStream.Write(subChunk2,0,4);

		fileStream.Close ();

	}


	public byte[] getFileStreamBytes() {
		//int streamLength = (int)fileStream.Length;
		//Debug.Log ("Stream length::" + streamLength);
		//byte[] returnedBytes = new byte[streamLength];
		//fileStream.Read (returnedBytes, 0, streamLength);
		//fileStream.Close ();
		byte[] altReturnedBytes = File.ReadAllBytes (fileName);

		//Debug.Log ("Length of returned bytes:" + returnedBytes.Length);
		Debug.Log ("Length of alt returned bytes:" + altReturnedBytes.Length);



		fakeFile = new FileStream (fakeFileName, FileMode.Create);
		fakeFile.Write (altReturnedBytes, 0, altReturnedBytes.Length);
		fakeFile.Close ();

		return altReturnedBytes;
	}
			
}



