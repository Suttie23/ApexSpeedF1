using F1_Telemetry_App;

namespace F1_Telemetry_App
{
    struct PacketCarStatusData
    {
        // Packet is formed of the header (PacketHeader.cs) and the car status data (CarStatusData.cs) to form the full CarStatusData Packet!
        public PacketHeader header;
        public CarStatusData[] carStatusData;

        public static PacketCarStatusData FromByteArray(byte[] data)
        {
            var packetCarStatusData = new PacketCarStatusData
            {
                header = PacketHeader.FromByteArray(data),
                carStatusData = new CarStatusData[22]
            };

            // For all cars
            for (int i = 0; i < 22; i++)
            {
                packetCarStatusData.carStatusData[i] = CarStatusData.FromByteArray(data, 24 + i * 60);
            }

            // Return the car status
            return packetCarStatusData;
        }
    }
}