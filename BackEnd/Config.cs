namespace BackEnd {
    public class Config {
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        public List<string> InputPaths { get; private set; } = new();
        public string OutputPath { get; set; } = string.Empty;
        //možno dále rozšířit o další věci, které jsou pak passnuté
        //backendu - např. separator char pro CSV nebo výběr mezi
        //exportem do CSV a XLSX.

        public Config() {
            InputPaths = new();
        }

        public bool AddInputFile(string path) {
            //zkontrolovat, zda už soubor není v seznamu
            //pokud ano, jednoduše return
            //pokud ne, přidat
            if (InputPaths.Contains(path)) {
                return false;
            }
            InputPaths.Add(path);
            return true;
        }

        public void RemoveInputFile(string path) {
            //jednoduchý remove
            InputPaths.Remove(path);
        }

        public bool ValidateConfig() {
            if (InputPaths is null || InputPaths.Count == 0) {
                _log.Error("Není stanoven žádný vstupní soubor.");
                return false;
            }
            if (string.IsNullOrEmpty(OutputPath)) {
                _log.Error("Není stanovena výstupní cesta.");
                return false;
            }
            return true;
        }
    }
}