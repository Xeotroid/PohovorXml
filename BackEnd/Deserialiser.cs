using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd {
    public class Deserialiser {
        public static bool Start(Config config) {
            if (config.InputPaths.Count == 0 || config.OutputPath == string.Empty) {
                return false;
            }
            return true;
        }
    }
}
