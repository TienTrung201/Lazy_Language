using System.Globalization;

namespace NTTRUNG_Lazy_Languages_Middleware
{
    public class LocalizationMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        #endregion

        #region Constructor
        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thay đổi ngôn ngữ
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// CreatedBy: NTTrung (23/07/2023)
        public async Task InvokeAsync(HttpContext context)
        {
            var requestLanguages = context.Request.Headers["ContentLanguage"];


            if (requestLanguages.Count == 1)
            {
                CultureInfo.CurrentCulture = new CultureInfo(requestLanguages[0]);
                CultureInfo.CurrentUICulture = new CultureInfo(requestLanguages[0]);
            }


            await _next(context);
        }
        #endregion
    }
}

