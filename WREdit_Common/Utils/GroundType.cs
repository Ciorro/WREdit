namespace WREdit.Common.Utils
{
    public enum GroundType
    {
        None, Dirt, Gravel, Asphalt, Airport
    }

    public static class GroundTypeExtensions
    {
        public static string ToPropertyString(this GroundType groundType)
        {
            return groundType switch
            {
                GroundType.Dirt => "MUDROAD",
                GroundType.Gravel => "PEDESTRIAN",
                GroundType.Asphalt => "ROAD",
                GroundType.Airport => "AIRPORT",
                _ => ""
            };
        }
    }
}
