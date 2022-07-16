﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Common.Constants
{
    public static class APIConstant
    {

        #region Controller

        public const string ApiUser = "api/User";

        #endregion

        #region Customerr

        public const string CustomerRegister = "Register";
        public const string CustomerLogin = "Login";

        #endregion

        #region User

        public const string UserLogin = "Login";
        public const string UserRegister = "Register";

        #endregion

        #region MovieCensorship
        public const string GetMovieCensorship  = "GetAllMovieCensorship";
        #endregion

        #region MyRegion

        public const string GetMovieGenre = "GetAllMovieGenre";

        #endregion
    }
}
