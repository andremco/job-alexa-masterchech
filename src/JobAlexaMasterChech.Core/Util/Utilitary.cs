using System;
using System.Text.RegularExpressions;

namespace JobAlexaMasterChech.Core.Util
{
    public static class Utilitary
    {
        public static int GetExternCodeFromUrl(this string valueUrl)
        {
            if (string.IsNullOrEmpty(valueUrl)) return 0;

            var resultValue = Regex.Match(valueUrl, @"\d+").Value;

            return int.Parse(resultValue);
        }

        public static string RemoveInvalidSentenceFromString(this string value)
        {
            string[] blackList = { "Li e aceito a política de privacidade", "\n&frac12;", "\n", "&frac12;", "&#8531" };
            if (string.IsNullOrEmpty(value)) return string.Empty;

            string validResult = Regex.Unescape(value);

            foreach (var toRemove in blackList)
            {
                var rgx = new Regex(toRemove);
                validResult = rgx.Replace(validResult, "", 1);
            }

            return validResult;
        }
    }
}
