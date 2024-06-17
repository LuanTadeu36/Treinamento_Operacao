namespace TreinamentoAPI.Exceções
{
    public class TratamentoExcessao
    {
        public TratamentoExcessao() 
        {
            
        }

        public void MakeErrorString(IEnumerable<string> erros) 
        {
            string msgErro = "";
            foreach (var e in erros)
            {
                msgErro += e + "\n";
            }
            throw new Exception(msgErro);
        }
    }
}
