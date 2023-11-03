using System.Runtime.CompilerServices;

namespace BackEnd {
    public class LogHelper {
        /// <summary>
        /// Vrátí log4net logger s absolutní cestou k souboru s volanou metodou.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static log4net.ILog GetLogger([CallerFilePath]string filename = "") {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}
