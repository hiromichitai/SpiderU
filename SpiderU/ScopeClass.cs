using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace SpiderU {

	public class TraceClass {	// Trace class. Each Scope has at least one trace.
		private int ChannelNumber;
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

		public int ChannelNo {
			get { return ChannelNumber; }
			set { ChannelNumber = value; }
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


		public double this[int DIndex] {
			get {
				return TraceDataArray[DIndex];
			}
			set {
				TraceDataArray[DIndex] = value;
			}
		}

		public double[] Data {
			get {
				return TraceDataArray;
			}
		}
	}


	public abstract class ScopeClass {
// ScopeClass is the abstract base class of all scope

		protected ComPortClass ComPort;
		protected int NumberOfChannel;
		protected int RecordLength;
		protected double SamplingTime;
		protected string ModelNameString;
		protected string ScopeIDString;
		protected string ScopeCommentString;
		protected string AcquisitionDateTimeString;
		protected List<TraceClass> TraceList;
		protected bool DataValid;

		public ScopeClass(ComPortClass MyDevice,string ModelName,int NumChannel){
			ComPort = MyDevice;
			DataValid = false;
			ModelNameString = ModelName;
			NumberOfChannel = NumChannel;
			TraceList = new List<TraceClass>(NumberOfChannel);
			for (int TIndex = 0; TIndex < NumChannel; TIndex++) {
				TraceClass NewTrace = new TraceClass();
				NewTrace.ChannelNo = TIndex + 1;
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

		public bool DataIsValid {
			get { return DataValid;  }
			set { DataValid = value;  }
		}

		public int DataLength {
			get {
				int DLength = 0;
				foreach (TraceClass Trace in TraceList.FindAll((trace) => (trace.IsOn))) {
					if (DLength == 0) {
						DLength = Trace.DataLength;
					} else {
						if (DLength != Trace.DataLength) { // something went wrong
							ErrorDialog EDialog = new ErrorDialog("UIMSGDATAINCONSIST", true);
						}
					}
				}
				return DLength;
			}
		}

		public string ChannelLabel(int Channel) {
			return TraceList[Channel].TraceLabel;
		}

		public string ChannelUnit(int Channel) {
			return TraceList[Channel].TraceUnit;
		}


		public double STime(int Index) {
			return SamplingTime*Index;
		}

		public double SampleTime {
			get { return SamplingTime; }
		}

		public int NumChannel {	// returns number of channel
			get { return NumberOfChannel; }
		}

		abstract public void GetSettings();	// Get current status

		protected abstract void GetData() ;			// Get  data base method

		public void AcquireData() { // this is the public interface for other class
			GetData();
			AcquisitionDateTimeString = Convert.ToString(DateTime.Now);
			DataValid = true;
		}

		public List<TraceClass> OnTrace {
			get { return TraceList.FindAll((trace) => (trace.IsOn)); }
		}

		public void DrawScope(object sender, System.Windows.Forms.PaintEventArgs e) {
			if (DataValid) {
				Rectangle ClipRectangle = e.ClipRectangle;
				Graphics Graph = e.Graphics;
				foreach (TraceClass Trace in OnTrace) {
					double MaxY = Trace[0];
					double MinY = Trace[0];
					for (int Index = 0; Index < Trace.DataLength; Index++) {
						if (Trace[Index] > MaxY) {
							MaxY = Trace[Index];
						}
						if (Trace[Index] < MinY) {
							MinY = Trace[Index];
						}
					}
					if (MaxY == MinY) {
						MaxY += 1.0;
						MinY -= 1.0;
					}

					Graph.ResetTransform();
					Graph.ScaleTransform(1.0f * ClipRectangle.Width / Trace.DataLength, -1.0f * ClipRectangle.Height / (float)(MaxY - MinY), MatrixOrder.Append);
					Graph.TranslateTransform(0, (float)ClipRectangle.Height, MatrixOrder.Append);
					for (int DIndex = 1; DIndex < Trace.DataLength; DIndex++) {
						Graph.DrawLine(Pens.Black, (float)(DIndex-1), (float)Trace[DIndex-1], (float)(DIndex), (float)Trace[DIndex]);
					}
				}
			}

		}

	}
}
