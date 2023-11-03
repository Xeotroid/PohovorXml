namespace BackEnd {
    static class StrExtensions {
        /// <summary>
        /// Převede string na malá písmena s velkým písmenem na začátku.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstUpper(this string str) {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }

        /// <summary>
        /// Převede string na malá písmena s velkými písmeny na začátku slov a po pomlčce.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstLettersUpper(this string str) {
            if (string.IsNullOrEmpty(str)) return str;
            char[] separators = { ' ', '-' };
            var chars = str.ToLower().ToList();

            chars[0] = char.ToUpper(chars[0]);
            for (int i = 1; i < chars.Count; i++) {
                if (separators.Contains(chars[i-1])) {
                    chars[i] = char.ToUpper(chars[i]);
                }
            }
            return new string(chars.ToArray());
        }
    }
}
