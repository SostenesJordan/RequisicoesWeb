using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace RequisicoesWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnviaRequisicaoGET();
            EnviaRequisicaoPOST();           
        }

        public static void EnviaRequisicaoGET()
        {
            var requisicaoWeb = WebRequest.CreateHttp("https://pje1g.tjrn.jus.br/consultapublica/ConsultaPublica/listView.seam");
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                Console.WriteLine(post.Id + " " + post.title + " " + post.body);
                //Console.ReadLine();
                streamDados.Close();
                resposta.Close();
            }
            //Console.ReadLine();
        }

        public static void EnviaRequisicaoPOST()
        {
            string dadosPOST = "title=macoratti";
            dadosPOST = dadosPOST + "&body=teste de envio de post";
            dadosPOST = dadosPOST + "&userId=1";

            var dados = Encoding.UTF8.GetBytes(dadosPOST);

            var requisicaoWeb = WebRequest.CreateHttp("https://jsonplaceholder.typicode.com/posts");

            requisicaoWeb.Method = "POST";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            requisicaoWeb.ContentLength = dados.Length;
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";

            //precisamos escrever os dados post para o stream
            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();
            }

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();

                var post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                Console.WriteLine(post.Id + " " + post.title + " " + post.body);
                streamDados.Close();
                resposta.Close();
            }
            
            Console.ReadLine();
        }
    }
}
