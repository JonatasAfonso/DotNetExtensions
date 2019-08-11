namespace DotNetExtensions
{
    public static class StringHelper
    {
        /// <summary>
        /// Assures that an choosen string finalize exactly with an especific char
        /// usefull in pathStrings
        /// </summary>
        /// <param name="value"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string FinalizeWithChar(this string value, string character)
        {
            if (value.EndsWith(character))
                return value;
            return value + character;
        }

        /// <summary>
        /// Remove all accents from a text sequence and returns text without diacritcs (char instead of accent-char version). E.g. á turn into a, õ into o ...
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            return result;
        }

        /// <summary>
        /// Remove all special characters from an given text.
        /// Accepts only a-z, A-Z, 0-9 and _
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string text)
        {
            var result = Regex.Replace(text, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
            return result;
        }

        /// <summary>
        /// Remove all accents from a text sequence and returns text without diacritcs (char instead of accent-char version). E.g. á turn into a, õ into o ...
        /// Alson remove all special characters from an given text.
        /// Accepts only a-z, A-Z, 0-9 and _
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveDiacriticsAndSpecialCharacteres(this string text)
        {
            var withOutDiacritics = RemoveDiacritics(text);
            var result = RemoveSpecialCharacters(withOutDiacritics);

            return result;
        }
                    
        /// <summary>
        /// Implements comparison between two strings avoiding diacritcs and specialCharacteres
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool EqualsButSpecialCharacteres(this string source, string destination)
        {
            var sourcePrepared = source.RemoveDiacriticsAndSpecialCharacteres();
            var destinationPrepared = destination.RemoveDiacriticsAndSpecialCharacteres();

            var result = sourcePrepared == destinationPrepared;
            return result;
        }           

        /// <summary>
        /// Implements comparison among one string and a list
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool EqualsButSpecialCharacteres(this string source, List<string> destination)
        {
            var sourcePrepared = source.RemoveDiacriticsAndSpecialCharacteres();                                  
            var hasntAtLeastOneDifferent = destination.All(x => sourcePrepared.ToUpper() == x.ToUpper().RemoveDiacriticsAndSpecialCharacteres());

            return hasntAtLeastOneDifferent;
        }
    }
}