namespace ManageCollections.Application.Models.ResponseModels
{
    public class ResponseCore<T>
    {
        public ResponseCore(bool isSuccess, object errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public ResponseCore(T result)
        {
            Result = result;
        }

        public ResponseCore()
        {

        }

        public bool IsSuccess { get; set; } = true;
        public object Errors { get; set; }
        public T Result { get; set; }
    }
}
