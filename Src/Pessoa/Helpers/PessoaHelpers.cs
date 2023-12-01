
namespace Src.Pessoa.Helpers
{

    public class PessoaHelpers
    {
        private static readonly Random random = new Random();
        private readonly string NomeArquivo = "numero_unico.txt";
        private readonly string NomePasta = "01_NUMERO_UNICO";

        public string GerarNumeroAleatorio(string email)
        {

            DateTime now = DateTime.Now;
            int currentTime = now.Day + now.Hour + now.Second + now.Millisecond + now.Microsecond;
            int numeroUnico = currentTime + random.Next(1 - currentTime, 10000 - currentTime);
            string numero = numeroUnico.ToString();
            SalvarNumeroEmArquivo(numero, email);

            return numero;
        }

        public void SalvarNumeroEmArquivo(string numero, string email)
        {
            try
            {
                string diretorioProjeto = Directory.GetCurrentDirectory();
                string diretorioNumeros = Path.Combine(diretorioProjeto, NomePasta);

                if (!Directory.Exists(diretorioNumeros))
                {
                    Directory.CreateDirectory(diretorioNumeros);
                }

                string caminhoCompleto = Path.Combine(diretorioNumeros, NomeArquivo);

                using (StreamWriter writer = File.AppendText(caminhoCompleto))
                {
                    writer.WriteLine($"Número: {numero}");
                    writer.WriteLine($"Usúario: {email}");
                    writer.WriteLine(" ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o número no arquivo: {ex.Message}");
                throw new Exception($"Ocorreu um Erro ao salvar o número no arquivo: {ex.Message}");
            }
        }

    }
}