using System;
using UnityEngine;
using System.Collections.Generic;

namespace BingMapsEntities
{
	
	public class BingMapsEntityId :MonoBehaviour
	{
		public static Dictionary<string, string> map;
		public static Dictionary<string, string> reverseMap;

		public int[] codes = {2084, 3578, 4013, 4100, 4170, 4444, 4482, 4493, 4580, 4581, 5000, 5400, 5511, 5512, 5540, 5571, 5800, 5813, 5999, 6000, 6512,
			7011,
			7012,
			7013,
			7014,
			7389,
			7510,
			7520,
			7521,
			7522,
			7538,
			7832,
			7897,
			7929,
			7933,
			7940,
			7947,
			7985,
			7990,
			7992,
			7994,
			7996,
			7997,
			7998,
			7999,
			8060,
			8200,
			8211,
			8231,
			8410,
			8699,
			9121,
			9211,
			9221,
			9517,
			9522,
			9525,
			9530,
			9535,
			9537,
			9545,
			9560,
			9565,
			9567,
			9568,
			9583,
			9590,
			9591,
			9592,
			9593,
			9710,
			9714,
			9715,
			9718,
			9719,
			9986,
			9987,
			9988,
			9991,
			9992,
			9993,
			9994,
			9995,
			9996,
			9998,
			9999,
		};

		public string[] entities = {"Winery",
			"ATM",
			"Train Station",
			"Commuter Rail Station",
			"Bus Station",
			"Named Place",
			"Ferry Terminal",
			"Marina",
			"Public Sports Airport",
			"Airport",
			"Business Facility",
			"Grocery Store",
			"Auto Dealerships",
			"Auto Dealership-Used Cars",
			"Petrol/Gasoline Station",
			"Motorcycle Dealership",
			"Restaurant",
			"Nightlife",
			"Historical Monument",
			"Bank",
			"Shopping",
			"Hotel",
			"Ski Resort",
			"Other Accomodation",
			"Ski Lift",
			"Tourist Information",
			"Rental Car Agency",
			"Parking Lot",
			"Parking Garage/House",
			"Park & Ride",
			"Auto Service & Maintenance",
			"Cinema",
			"Rest Area",
			"Performing Arts",
			"Bowling Centre",
			"Sports Complex",
			"Park/Recreation Area",
			"Casino",
			"Convention/Exhibition Centre",
			"Golf Course",
			"Civic/Community Centre",
			"Amusement Park",
			"Sports Centre",
			"Ice Skating Rink",
			"Tourist Attraction",
			"Hospital",
			"Higher Education",
			"School",
			"Library",
			"Museum",
			"Automobile Club",
			"City Hall",
			"Court House",
			"Police Station",
			"Campground",
			"Truck Stop/Plaza",
			"Government Office",
			"Post Office",
			"Convenience Store",
			"Clothing Store",
			"Department Store",
			"Home Specialty Store",
			"Pharmacy",
			"Specialty Store",
			"Sporting Goods Store",
			"Medical Service",
			"Residential Area/Building",
			"Cemetery",
			"Highway Exit",
			"Transportation Service",
			"Weigh Station",
			"Cargo Centre",
			"Military Base",
			"Animal Park",
			"Truck Dealership",
			"Home Improvement & Hardware Store",
			"Consumer Electronics Store",
			"Office Supply & Services Store",
			"Industrial Zone",
			"Place of Worship",
			"Embassy",
			"County Council",
			"Bookstore",
			"Coffee Shop",
			"Hamlet",
			"Border Crossing",
		};

	
			
		void Awake() {
			map = new Dictionary<string, string> ();
			reverseMap = new Dictionary<string, string> ();
			for (int x = 0; x < codes.Length; x++) {
				map.Add ((codes [x]).ToString(), entities[x]);
				reverseMap.Add (entities [x], codes [x].ToString ());
			}

			//Debug.Log (map["9999"]);
		}
		
		public static string getEntityNameFromId(string id) {
			return map [id];
		}

		public static string getIdFromEntityName(string entityName) {
			return reverseMap [entityName];
		}

	}
}

