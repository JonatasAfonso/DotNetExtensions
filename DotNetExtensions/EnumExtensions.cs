namespace DotNetExtensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[])
            {
                var attributes = (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]);
                var result = attributes?.First()?.Description;
                return result;
            }

            return value.ToString();
        }
    }
}