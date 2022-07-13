using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Common.Constants
{
    public static class APIConstant
    {
        public const string CustomerRegister = "Register";
        public const string CustomerLogin = "Login";

        #region MovieCensorship
        public const string GetMovieCensorship  = "GetAllMovieCensorship";
        #endregion

        #region MyRegion

        public const string GetMovieGenre = "GetAllMovieGenre";

        #endregion
    }
}
