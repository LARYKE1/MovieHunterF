using System.Text;

namespace MovieHunter.Infrastructure
{
    public class EncodingUtility
    {
        public static string EncodeId(int id)
        {
            string encodedId = Convert.ToBase64String(Encoding.ASCII.GetBytes(id.ToString()));
            return encodedId;
        }

        public static int DecodeId(string encodedId)
        {
            byte[] bytes = Convert.FromBase64String(encodedId);
            string decodedIdString = Encoding.ASCII.GetString(bytes);
            int decodedId = int.Parse(decodedIdString);
            return decodedId;
        }
    }
}