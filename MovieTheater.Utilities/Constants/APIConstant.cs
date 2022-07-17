using System;
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
        public const string ApiMovie = "api/Movie";
        public const string ApiMovieCensorship = "api/MovieCensorship";
        public const string ApiAuditorium = "api/Auditorium";
        public const string ApiSeat = "api/Seat";
        public const string ApiScreening = "api/Screening";

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

        public const string GetMovieCensorship = "GetAllMovieCensorship";

        #endregion

        #region MovieGenre

        public const string GetMovieGenre = "GetAllMovieGenre";

        #endregion

        #region Movie

        public const string GetMoviePaging = "GetMoviePaging";
        public const string MovieCreate = "Create";
        public const string MovieUpdate = "Update";
        public const string MovieDelete = "Delete";
        public const string MovieGetById = "AuditoriumGetById";


        #endregion

        #region Auditorium

        public const string AuditoriumGetPaging = "GetPaging";
        public const string AuditoriumCreate = "Create";
        public const string AuditoriumUpdate = "Update";
        public const string AuditoriumDelete = "Delete";
        public const string AuditoriumGetById = "AuditoriumGetById";
        public const string AuditoriumGetAll = "GetAll";
        #endregion

        #region Seat

        public const string SeatUpdateInRoom = "UpdateInRoom";
        public const string SeatGetAllInRoom = "GetAllInRoom";

        #endregion

        #region Screening

        public const string ScreeningGetPaging = "GetPaging";
        public const string ScreeningCreate = "Create";
        public const string ScreeningUpdate = "Update";
        public const string ScreeningDelete = "Delete";
        public const string ScreeningGetById = "GetById";
        public const string ScreeningGetScreeningInDate = "GetSceeningInDate";
        


        #endregion
    }
}