namespace API.Errors
{
    public class ApiErrorResponse
    {
    public ApiErrorResponse()
    {
    }

    public ApiErrorResponse(int status, string message = null)
        {
            Status = status;
            Message = message ?? DefaultMessage(status);
        }

        public int Status { get; set; }
        public string Message { get; set; }

        private string DefaultMessage(int statusCode) {
            switch (statusCode)
            {
                case 400:
                    return "Request inválido";
                case 401:
                    return "Você não está autorizado";
                case 403:
                    return "Você não tem permissão";
                case 404:
                    return "Nada foi encontrado";
                case 500:
                    return "Erro inesperado. Tente novamente.";
                default:
                    return "Erro invesperado";
            }
        }
    }
}