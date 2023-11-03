namespace BackEnd {
    static class StrExtensions {
        public static string FirstUpper(this string str) {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }
    }
}
