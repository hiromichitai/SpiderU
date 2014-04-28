using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;

namespace SpiderU {

	public class TraceClass {	// Trace class. Each Scope has at least one trace.
		private double MultiplyFactor = 1.0;
		private string TraceLabelString;
		private string TraceUnitString;
		private double[] TraceDataArray;
		bool TraceOn;

		public double Multiplier {
			get { return this.MultiplyFactor; }
			set { this.MultiplyFactor = value; } 
		}

		public string TraceLabel {
			get { return this.TraceLabelString; }
			set { this.TraceLabelString = value; }
		}

		public string TraceUnit {
			get { return this.TraceUnitString; }
			set { this.TraceUnitString = value; }
		}

		public int DataLength {
			get {
					if (TraceDataArray != null) {  
					return this.TraceDataArray.Length;
					} else {
						return 0;
					}
				}
			set { TraceDataArray = new double[value]; }
		}


		public bool IsOn {
			get { return this.TraceOn; }
			set { this.TraceOn = value; }
		}

		public double[] Data() {
			return TraceDataArray;
		}

		public double this[int DIndex] {
			get {
				return TraceDataArray[DIndex];
			}
			set {
				TraceDataArray[DIndex] = value;
			}
		}
	}


	public abstract class ScopeClass {
// ScopeClass is the abstract base class of all scope

		protected Device GPIBDevice;
		protected int NumberOfChannel;
		protected int RecordLength;
		protected double SamplingTime;
		protected string ModelNameString;
		protected string ScopeIDString;
		protected string ScopeCommentString;
		protected string AcquisitionDateTimeString;
		protected List<TraceClass> TraceList;

		public ScopeClass(Device MyDevice,string ModelName,int NumChannel){
			GPIBDevice = MyDevice;
			ModelNameString = ModelName;
			NumberOfChannel = NumChannel;
			TraceList = new List<TraceClass>(NumberOfChannel);
			for (int Channel = 0; Channel < NumChannel; Channel++) {
				TraceClass NewTrace = new TraceClass();
				TraceList.Add(NewTrace);
			}
		}

		public string ModelName {
			get { return this.ModelNameString; }
		}

		public string ID {
			get { return this.ScopeIDString; }
			set { this.ScopeIDString = value; }
		}

		public string Comment {
			get {
 				string CommentString = this.ScopeCommentString;
				if(Properties.Settings.Default.includeDateTime){
					CommentString += " " + AcquisitionDateTimeString;
				}
				if(Properties.Settings.Default.includeDateTime){
					CommentString += " " + this.ModelName;
				}
				return CommentString; 
			}
			set { this.ScopeCommentString = value; }
		}

		public List<TraceClass> Channel {
			get { return TraceList;}
		}

		public int DataLength {
			get {
				int DLength = 0;
				for (int TraceIndex = 0; TraceIndex < TraceList.Count; TraceIndex++) {
					if (TraceList[TraceIndex].IsOn) {
						if(DLength == 0){
							DLength = TraceList[TraceIndex].DataLength;
						} else {
							if(DLength != TraceList[TraceIndex].DataLength){ // something went wrong
								ErrorDialog EDialog = new ErrorDialog("UIMSGDATAINCONSIST",true);
							}
						}
					}
				}
				return DLength;
			}
		}


		public double[] ChannelData(int Channel){
			return TraceList[Channel].Data();
		}

		public bool ChannelOn(int Channel) {
			return TraceList[Channel].IsOn;
		}

		public string ChannelLabel(int Channel) {
			return TraceList[Channel].TraceLabel;
		}

		public string ChannelUnit(int Channel) {
			return TraceList[Channel].TraceUnit;
		}

		public double ChannelMultiplier(int Channel) {
			return TraceList[Channel].Multiplier;
		}

		public int NumOnChannel {
			get { 
				int OnChannelCount = 0;
				for (int TraceIndex = 0; TraceIndex < TraceList.Count; TraceIndex++) {
					if (TraceList[TraceIndex].IsOn) {
						OnChannelCount++;
					}
				}
				return OnChannelCount;
			 }
		}

		public TraceClass NthOnChannel(int Index) {
			int OnChannelCount = 0;
			for (int TraceIndex = 0; TraceIndex < TraceList.Count; TraceIndex++) {
				if (TraceList[TraceIndex].IsOn) {
					if(Index == OnChannelCount){
						return TraceList[TraceIndex];
					}
					OnChannelCount++;
				}
			}
			return null;
		}

		public int NthOnChannelNumber(int Index) {
			int OnChannelCount = 0;
			for (int TraceIndex = 0; TraceIndex < TraceList.Count; TraceIndex++) {
				if (TraceList[TraceIndex].IsOn) {
					if (Index == OnChannelCount) {
						return TraceIndex;
					}
					OnChannelCount++;
				}
			}
			return -1;
		}

		public double STime(int Index) {
			return SamplingTime*Index;
		}

		public double SampleTime {
			get { return SamplingTime; }
		}

		abstract public int NumChannel();	// returns number of channel
		abstract public void GetSettings();	// Get current status

		protected abstract void GetData() ;			// Get  data base method

		public void AcquireData() { // this is the public interface for other class
			GetData();
			AcquisitionDateTimeString = Convert.ToString(DateTime.Now);
		}


	}
}
