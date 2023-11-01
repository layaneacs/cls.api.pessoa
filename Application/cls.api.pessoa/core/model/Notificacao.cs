namespace cls.api.pessoa.core.model
{
    public class Notificacao
    {
        public Dictionary<string, string> Data { get;} = new Dictionary<string, string>();
        public bool HasNotificacao => Data.Any();

        public void AddNotificacao(string key, string message)
        {
            Data.Add(key, message);
        }
    }
}
