using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;
using System.Windows;


namespace SpiderU {

	public class DeviceListClass {
		const byte PRIMARYADDRESSLOWEST = 1;
		const byte PRIMARYADDRESSHIGHEST = 16;

		private Board GPIBBoard;
        private AddressCollection FreeDeviceAddressList;

        public DeviceListClass() {
            GPIBBoard = new Board();
			GPIBBoard.SendInterfaceClear();
			FreeDeviceAddressList = new AddressCollection();
			for(byte PAddress = PRIMARYADDRESSLOWEST; PAddress < PRIMARYADDRESSHIGHEST; PAddress++){
				FreeDeviceAddressList.Add(new Address(PAddress));
			}
            GetDeviceList();
            GPIBBoard.Dispose();
        }

        public int NumDevice() {
            return FreeDeviceAddressList.Count;
        }

		public int NumFreeDevice() {
			return FreeDeviceAddressList.Count;
		}

        public void GetDeviceList() {
            try {
				FreeDeviceAddressList = GPIBBoard.FindListeners(FreeDeviceAddressList);
            }
            catch (System.Exception Ex) {
                throw (Ex);
            }
        }

		public void UseDevice(Device GPIBDevice) {
			if (FreeDeviceAddressList.Count > 0) {
				for (int Index = 0; Index < FreeDeviceAddressList.Count; Index++) {
					if(GPIBDevice.PrimaryAddress == FreeDeviceAddressList[Index].PrimaryAddress){
						FreeDeviceAddressList.RemoveAt(Index);
					}
				}
			}
		}


        public byte FreeDevicePrimaryAddress(int Index) {
            return FreeDeviceAddressList[Index].PrimaryAddress;
        }

		public byte FreeDeviceSecondaryAddress(int Index) {
            return FreeDeviceAddressList[Index].SecondaryAddress;
        }

 
    }
}
