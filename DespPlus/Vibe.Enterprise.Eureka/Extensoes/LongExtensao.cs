namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class LongExtensao
    {
        public static string TamanhoArquivoLegivel(this long tamanhoBytes)
        {
            long tamanhoAbsoluto = (tamanhoBytes < 0 ? -tamanhoBytes : tamanhoBytes);

            if (tamanhoAbsoluto >= 0x1000000000000000) // Exabyte
                return $"{((double)(tamanhoBytes >> 50)) / 1024:0} EB";

            if (tamanhoAbsoluto >= 0x4000000000000) // Petabyte
                return $"{((double)(tamanhoBytes >> 40)) / 1024:0} PB";

            if (tamanhoAbsoluto >= 0x10000000000) // Terabyte
                return $"{((double)(tamanhoBytes >> 30)) / 1024:0} TB";

            if (tamanhoAbsoluto >= 0x40000000) // Gigabyte
                return $"{((double)(tamanhoBytes >> 20)) / 1024:0} GB";

            if (tamanhoAbsoluto >= 0x100000) // Megabyte
                return $"{((double)(tamanhoBytes >> 10)) / 1024:0} MB";

            if (tamanhoAbsoluto >= 0x400) // Kilobyte
                return $"{(double)tamanhoBytes / 1024:0} KB";

            return tamanhoBytes.ToString("0 B"); // Byte
        }
    }
}
