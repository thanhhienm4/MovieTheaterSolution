namespace MovieTheater.Common.Constants
{
    public static class ApiConstant
    {
        #region Controller

        public const string ApiUser = "api/User";
        public const string ApiCustomer = "api/Customer";
        public const string ApiMovie = "api/Movie";
        public const string ApiMovieGenre = "api/MovieGenre";
        public const string ApiMovieCensorship = "api/MovieCensorship";
        public const string ApiAuditorium = "api/Auditorium";
        public const string ApiSeat = "api/Seat";
        public const string ApiScreening = "api/Screening";
        public const string ApiTime = "api/Time";
        public const string ApiTicketPrice = "api/TicketPrice";
        public const string ApiSurcharge = "api/Surcharge";
        public const string ApiTicket = "api/Ticket";
        public const string ApiRole = "api/Role";
        public const string ApiInvoice = "api/Invoice";
        public const string ApiStatistic  = "api/Statistic";

        #endregion Controller

        #region Customerr

        public const string CustomerRegister = "Register";
        public const string CustomerLogin = "Login";
        public const string CustomerTypeGetAll = "GetAllCustomerType";
        public const string CustomerForgotPassword = "ForgotPassword";
        public const string CustomerChangePassword = "ChangePassword";

        #endregion Customerr

        #region User

        public const string UserLogin = "Login";
        public const string UserRegister = "Register";


        #endregion User

        #region MovieCensorship

        public const string GetMovieCensorship = "GetAllMovieCensorship";

        #endregion MovieCensorship

        #region MovieGenre

        public const string GetMovieGenre = "GetAllMovieGenre";
        public const string MovieGenreGetByMovieId = "GetByMovieId";

        #endregion MovieGenre

        #region Movie

        public const string GetMoviePaging = "GetMoviePaging";
        public const string MovieCreate = "Create";
        public const string MovieUpdate = "Update";
        public const string MovieDelete = "Delete";
        public const string MovieGetById = "AuditoriumGetById";

        #endregion Movie

        #region Auditorium

        public const string AuditoriumGetPaging = "GetPaging";
        public const string AuditoriumCreate = "Create";
        public const string AuditoriumUpdate = "Update";
        public const string AuditoriumDelete = "Delete";
        public const string AuditoriumGetById = "AuditoriumGetById";
        public const string AuditoriumGetAll = "GetAll";
        public const string AuditoriumGetCoordinate = "GetCoordinate";

        #endregion Auditorium

        #region Seat

        public const string SeatUpdateInRoom = "UpdateInRoom";
        public const string SeatGetAllInRoom = "GetAllInRoom";
        public const string SeatGetListSeatReserve = "GetListSeatReverve";

        #endregion Seat

        #region Screening

        public const string ScreeningGetPaging = "GetPaging";
        public const string ScreeningCreate = "Create";
        public const string ScreeningUpdate = "Update";
        public const string ScreeningDelete = "Delete";
        public const string ScreeningGetById = "GetById";
        public const string ScreeningGetScreeningInDate = "GetSceeningInDate";

        #endregion Screening

        #region Time

        public const string TimeGetAll = "GetAll";
        public const string TimeCreate = "Create";
        public const string TimeUpdate = "Update";
        public const string TimeDelete = "Delete";
        public const string TimeGetById = "GetById";
        public const string TimePaging = "GetPaging";

        #endregion Time

        #region Ticket

        public const string TicketCreate = "create";
        public const string TicketUpdate = "update";
        public const string TicketDelete = "delete";

        #endregion Ticket

        #region role

        public const string RoleGetAll = "getAll";

        #endregion role

        #region Invoice

        public const string InvoiceCreate = "create";

        #endregion Invoice

        #region TicketPrivce

        public const string TicketPriceCreate = "create";
        public const string TicketPriceUpdate = "update";
        public const string TicketPriceDelete = "delete";
        public const string TicketPricePaging = "paging";
        public const string TicketPriceGetById = "getById";

        #endregion TicketPrivce

        #region Surcharge

        public const string SurchargeCreate = "create";
        public const string SurchargeUpdate = "update";
        public const string SurchargeDelete = "delete";
        public const string SurchargePaging = "paging";
        public const string SurchargeGetById = "getById";

        #endregion Surcharge

        #region ApiStatistic

        public const string StatisticGetRawData = "getRawData";
        public const string StatisticGetRevenueDayInWeek = "getRevenueDayInWeek";

        #endregion

    }
}