using System;
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
				if(SList[ScopeIndex].ScopeID == ScopeID){
					return SList[ScopeIndex];
				}
			}
			return null;
		}

		public static List<ScopeClass> ScopeList {
			get { return SList; }
		}


		public static ScopeClass CreateNewScope(Device NewDevice) {
			ScopeClass NewScope = null;
			NewDevice.Write("*IDN?");
			string IDString = NewDevice.ReadString();
			char [] Separators = {','};
			string [] IDSubStrings = IDString.Split(Separators);
			switch(IDSubStrings[0]){
				case("YOKOGAWA"):
					switch(IDSubStrings[1]){
						case("701510"):		// DL1540
						case("701520"):		// DL1540L
						case("701530"):		// DL1540C
						case("701540"):		// DL1540CL
							NewScope = new DLM2000(NewDevice,"DL1540", 4);
							break;
						case("701505"):		// DL1520
						case("701515"):		// DL1520L
							NewScope = new DLM2000(NewDevice, "DL1520", 2);
							break;
						case("701605"):		// DL1620
							NewScope = new DLM2000(NewDevice, "DL1620", 2);
							break;
						case("701610"):		// DL1640
						case("701620"):		// DL1640L
							NewScope = new DLM2000(NewDevice, "DL1640", 4);
							break;
						case("701705"):		// DL1720
						case("701715"):		// DL1720E
						case("701725"):		// DL1735E
							NewScope = new DLM2000(NewDevice, "DL1720", 2);
							break;
						case("701710"):		// DL1740
						case("701730"):		// DL1740E
						case("701740"):		// DL1740EL
						case("701680"):		// DL1740EL?
							NewScope = new DLM2000(NewDevice, "DL1740", 4);
							break;
						case("700410"):		// DL4080?
							NewScope = new DLM2000(NewDevice, "DL4080", 4);
							break;
						case("701210"):		// DL750
						case("701230"):		// DL750P
							NewScope = new DLM2000(NewDevice, "DL750", 4);
							break;
						case("DL850"):		// DL850
						case("DL850V"):		// DL850
							NewScope = new DLM2000(NewDevice, "DL850", 4);
							break;
						case("701820"):		// DL708E
							NewScope = new DLM2000(NewDevice, "DL708", 8);
							break;
						case("701830"):		// DL716
						case("701831"):		// DL716
							NewScope = new DLM2000(NewDevice, "DL716", 16);
							break;
						case("710105"):		// DLM2022
							NewScope = new DLM2000(NewDevice, "DLM2022", 2);
							break;
						case ("710115"):		// DLM2032
							NewScope = new DLM2000(NewDevice, "DLM2032", 2);
							break;
						case ("710125"):		// DLM2052
							NewScope = new DLM2000(NewDevice, "DLM2052", 2);
							break;
						case("710110"):		// DLM2024
							NewScope = new DLM2000(NewDevice, "DLM2024", 4);
							break;
						case ("710120"):		// DLM2034
							NewScope = new DLM2000(NewDevice, "DLM2034", 4);
							break;
						case ("710130"): 	// DLM2054	
							NewScope = new DLM2000(NewDevice, "DLM2054", 4);
							break;
					}
					break;
				case("LECROY"):
					 break;
			}
			if (NewScope != null) {
				NewScope.GetSettings();
				NewScope.ScopeID = string.Format("{0:s}({1:G})",NewScope.ModelName,NewDevice.PrimaryAddress);
				SList.Add(NewScope);
				return NewScope;
			} else {
				return null;
			}
		}

		public static bool GetWaveform() {
			for (int ScopeIndex = 0; ScopeIndex < SList.Count; ScopeIndex++) {
				SList[ScopeIndex].GetData();
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
