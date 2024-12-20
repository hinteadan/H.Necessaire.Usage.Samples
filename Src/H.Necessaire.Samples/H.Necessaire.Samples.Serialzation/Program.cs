using H.Necessaire.Serialization;

namespace H.Necessaire.Samples.Serialzation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SomeData data = new SomeData { Name = "Hintee" };

            SomeData[] dataArray = [data, data];

            string jsonObjectString = data.ToJsonObject();

            string jsonArrayString = dataArray.ToJsonArray();

            SomeData deserializedData = jsonObjectString.JsonToObject<SomeData>();

            SomeData[] deserializedArray = jsonArrayString.JsonToObject<SomeData[]>();
        }
    }
}
