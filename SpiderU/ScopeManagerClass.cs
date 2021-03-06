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


		public static ScopeClass CreateNewScope(ComPortClass NewComPort) {
			ScopeClass NewScope = null;
			NewComPort.InitializeComPort();
			switch(NewComPort.VendorString){
				case("YOKOGAWA"):
					switch(NewComPort.ModelString){
						case("DL1720"):
							NewScope = new DL1700(NewComPort, "DL1720", 2);
							break;
						case("DL1740"):
							NewScope = new DL1700(NewComPort, "DL1740", 4);
							break;
						case("DL750"):
							NewScope = new DL750(NewComPort, "DL750", 16);
							break;
						case("DL850"):
							NewScope = new DLM2000(NewComPort, "DL850", 4);
							break;
						case("DLM2022"):		// DLM2022
							NewScope = new DLM2000(NewComPort, "DLM2022", 2);
							break;
						case ("DLM2038"):		// DLM2032
							NewScope = new DLM2000(NewComPort, "DLM2032", 2);
							break;
						case ("DLM2052"):		// DLM2052
							NewScope = new DLM2000(NewComPort, "DLM2052", 2);
							break;
						case("DLM2024"):		// DLM2024
							NewScope = new DLM2000(NewComPort, "DLM2024", 4);
							break;
						case ("DLM2034"):		// DLM2034
							NewScope = new DLM2000(NewComPort, "DLM2034", 4);
							break;
						case ("DLM2054"): 	// DLM2054	
							NewScope = new DLM2000(NewComPort, "DLM2054", 4);
							break;
					}
					break;
				case ("LECROY"):
					switch(NewComPort.ModelString){
						case ("WR6050A"):		// WaveRunner 6050
							NewScope = new WaveRunner6050(NewComPort,"WaveRunner6050", 4);
							break;
					}
					break;
				case("OWON"):
					switch (NewComPort.ModelString) {
						case("PDS5022"):
							NewScope = new PDS5022(NewComPort, "PDS5022", 2);
							break;
					}
					break;
			}
			if (NewScope != null) {
				NewScope.GetSettings();
				NewScope.ID = NewComPort.IDString;
				SList.Add(NewScope);
				return NewScope;
			} else {
				return null;
			}
		}

		public  static async Task<bool> GetWaveform() {
			if (SList.Count > 0) {
				for (int ScopeIndex = 0; ScopeIndex < SList.Count; ScopeIndex++) {
					SList[ScopeIndex].DataIsValid = false;
					SList[ScopeIndex].AcquireData();
				}
				return true;

			} else {
				return false;
			}
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
