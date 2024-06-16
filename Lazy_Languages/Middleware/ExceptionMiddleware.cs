using NTTRUNG_Lazy_Languages_Domain.Enum;
using NTTRUNG_Lazy_Languages_Domain.Resources.ErrorMessage;
using NTTRUNG_Lazy_Languages_Domain;

namespace NTTRUNG_Lazy_Languages_Middleware
{
    public class ExceptionMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion
        #region Constructor
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion
        #region Methods
        /// /// <summary>
        /// Phương thức xử lý ngoại lệ trong middleware
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        /// <returns>Task</returns>
        /// CreatedBy: NTTrung (Ngày 13/07/2023)
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// Xử lý và trả về ngoại lệ dưới dạng thông tin JSON
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        /// <param name="exception">Ngoại lệ được bắt</param>
        /// <returns>Task</returns>
        /// CreatedBy: NTTrung (Ngày 13/07/2023)
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            switch (exception)
            {

                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case ExistedConstrainException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((ExistedConstrainException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case DuplicateCodeException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((DuplicateCodeException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = "Duplicate Code!",
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case DuplicateCodeDetailException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((DuplicateCodeDetailException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = "Duplicate Code!",
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case BadRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((BadRequestException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = "Duplicate Code!",
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case AuthenticationException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((AuthenticationException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = "Authentication Exception",
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                case Unauthorized:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = ((Unauthorized)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = "Unauthorized",
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    Console.WriteLine(exception.Message);
                    await context.Response.WriteAsync(text: new BaseException()
                    {
                        ErrorCode = (int)ErrorCode.ErrorServerCode,
                        UserMessage = string.Format(ErrorMessage.Error500),
                        DevMessage = string.Format(ErrorMessage.Error500),
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }.ToString() ?? "");
                    break;
            }

        }
        #endregion

    }
}
