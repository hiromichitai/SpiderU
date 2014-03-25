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

	}


	public abstract class ScopeClass {
// ScopeClass is the abstract base class of all scope

		protected Device GPIBDevice;
		protected int NumberOfChannel;
		protected int RecordLength;
		protected double SamplingTime;
		protected string ModelNameString;
		protected string ScopeIDString;
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

		public string ScopeID {
			get { return this.ScopeIDString; }
			set { this.ScopeIDString = value; }
		}

		public List<TraceClass> Channel {
			get { return TraceList;}
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


		abstract public int NumChannel();	// returns number of channel
		abstract public void GetSettings();	// Get current status
		abstract public void GetData();	// Get channel data


	}
}
