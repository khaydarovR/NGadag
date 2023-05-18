namespace NGadag.DTO
{
    public static class Utils
    {
        public static string Substring(string value, int count)
        {
            if(value.Length > count)
            value = value.Substring(0, count);
            value += " ... ";
            return value;
        }

    }
}
