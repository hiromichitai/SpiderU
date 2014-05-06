﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;



namespace SpiderU {
	class ScopeManager {
		private static readonly ScopeManager instance = new ScopeManager();
   
		private static List<ScopeClass> SList;

		private ScopeManager() {
			SList = new List<ScopeClass>();			
		}

		public static ScopeManager Instance {
			get {
				return instance;
			}
		}

		public static ScopeClass GetScopeFromID(string ScopeID){
			for(int ScopeIndex=0; ScopeIndex < SList.Count; ScopeIndex++){
				if(SList[ScopeIndex].ID == ScopeID){
					return SList[ScopeIndex];
				}
			}
			return null;
		}

		public static List<ScopeClass> ScopeList {
			get { return SList; }
		}


		public static ScopeClass CreateNewScope(ComDeviceClass NewDevice) {
			ScopeClass NewScope = null;
			NewDevice.InitializeComDevice();
			switch(NewDevice.VendorString){
				case("YOKOGAWA"):
					switch(NewDevice.ModelString){
						case("DL1540"):
							NewScope = new DLM2000(NewDevice,"DL1540", 4);
							break;
						case("DL1520"):
							NewScope = new DLM2000(NewDevice, "DL1520", 2);
							break;
						case("DL1620"):
							NewScope = new DLM2000(NewDevice, "DL1620", 2);
							break;
						case("DL1640"):
							NewScope = new DLM2000(NewDevice, "DL1640", 4);
							break;
						case("DL1720"):
							NewScope = new DLM2000(NewDevice, "DL1720", 2);
							break;
						case("DL1740"):
							NewScope = new DLM2000(NewDevice, "DL1740", 4);
							break;
						case("DL4080"):
							NewScope = new DLM2000(NewDevice, "DL4080", 4);
							break;
						case("DL750"):
							NewScope = new DLM2000(NewDevice, "DL750", 4);
							break;
						case("DL850"):
							NewScope = new DLM2000(NewDevice, "DL850", 4);
							break;
						case("DL708"):
							NewScope = new DLM2000(NewDevice, "DL708", 8);
							break;
						case("DL716"):
							NewScope = new DLM2000(NewDevice, "DL716", 16);
							break;
						case("DLM2022"):		// DLM2022
							NewScope = new DLM2000(NewDevice, "DLM2022", 2);
							break;
						case ("DLM2038"):		// DLM2032
							NewScope = new DLM2000(NewDevice, "DLM2032", 2);
							break;
						case ("DLM2052"):		// DLM2052
							NewScope = new DLM2000(NewDevice, "DLM2052", 2);
							break;
						case("DLM2024"):		// DLM2024
							NewScope = new DLM2000(NewDevice, "DLM2024", 4);
							break;
						case ("DLM2034"):		// DLM2034
							NewScope = new DLM2000(NewDevice, "DLM2034", 4);
							break;
						case ("DLM2054"): 	// DLM2054	
							NewScope = new DLM2000(NewDevice, "DLM2054", 4);
							break;
					}
					break;
				case (" *IDN LECROY"):
					switch(NewDevice.ModelString){
						case ("WR6050A"):		// WaveRunner 6050
							NewScope = new WaveRunner6050(NewDevice,"WaveRunner6050", 4);
							break;
					}
					break;
				case("OWON"):
					switch (NewDevice.ModelString) {
						case("PDS5022"):
							NewScope = new PDS5022(NewDevice, "PDS5022", 2);
							break;
					}
					break;
			}
			if (NewScope != null) {
				NewScope.GetSettings();
				NewScope.ID = NewDevice.IDString;
				SList.Add(NewScope);
				return NewScope;
			} else {
				return null;
			}
		}

		public static bool GetWaveform() {
			for (int ScopeIndex = 0; ScopeIndex < SList.Count; ScopeIndex++) {
				SList[ScopeIndex].AcquireData();
			}
			return true;
		}

		public static bool AllScopeSyncable() {
			int RecordLength = SList[0].DataLength;
			double SampleTime = SList[0].SampleTime;
			for (int ScopeIndex = 1; ScopeIndex < SList.Count; ScopeIndex++) {
				if (RecordLength != SList[ScopeIndex].DataLength) {
					return false;
				}
				if((Math.Abs(SampleTime - SList[ScopeIndex].SampleTime)/SampleTime) > 1.0E-6){
					return false;
				} 
			}
			return true;
		}

	}

}
