using System;
using System.Collections.Generic;

namespace P79.Base.Commons
{
    public static class CommonConstants
    {

        public static Dictionary<string, Type> ConverterMapings = new Dictionary<string, Type>
        {
            { "string", typeof(string) },
            { "int", typeof(int) },
            { "bool",typeof(bool) },
            { "guid", typeof(Guid) },
            { "date", typeof(DateTime) },
            //{ "UnitLevel",typeof(UnitLevel)},
            //{ "QuizStatus",typeof(QuizStatus)},
            //{ "NotificationType",typeof(NotificationType)},
            //{ "NotificationCategory",typeof(NotificationCategory)},
            //{ "ContentStatus",typeof(ContentStatus)},
            //{ "ContentType",typeof(ContentType)},
            //{ "ContentTreeType",typeof(ContentTreeType)},
            { "raw", typeof(string)},
            //{ "UserType", typeof(UserType)},
            //{ "UserStatus", typeof(UserStatus)},
            //{ "AddendumType", typeof(AddendumType)},
            //{ "ApprovalStatus", typeof(ApprovalStatus)},
            //{ "CustomerApprovalStatus", typeof(CustomerApprovalStatus)},
            //{ "GuaranteeBankStatus", typeof(GuaranteeBankStatus)},
            //{ "FormType", typeof(FormType)}
        };
        public const string TOKEN_AUTHORIZATION_SCHEME = "Bearer";
        public const int TOKEN_EXPIRED = 30;//expired in minutes

        public const string APPLICATION_NAME = "X-APP";

        public const string NAMA_APLIKASI = "Sales Kit Web Admin";
        public const string ACTION_LOGIN = "Login";
        public const string ACTION_INSERT = "Insert";
        public const string ACTION_UPDATE = "Update";
        public const string ACTION_DELETE = "Delete";
        public const string ACTION_APPROVAL = "Approval";

        public const string MODULE_KONTEN = "Konten";
        public const string MODULE_UNIT_KERJA = "Unit Kerja";
        public const string MODULE_HAK_AKSES = "Manajemen Hak Akses";
        public const string MODULE_PENGATURAN_SISTEM = "Pengaturan Sistem";
        public const string MODULE_QUIZ = "Quiz";

        public const string FEATURE_KONTEN = "Konten";
        public const string FEATURE_ALUR_KONTEN = "Alur Konten";
        public const string FEATURE_UNIT_KERJA = "Unit Kerja";
        public const string FEATURE_USERS = "User";
        public const string FEATURE_ROLES = "Role";
        public const string FEATURE_API_CLIENTS = "Api Clients";
        public const string FEATRURE_BANK_SOAL = "Bank Soal";

        public const string EMAIL_VERIFICATION = "<body><div class = 'cnk-line-wrapper'><center><img src='cid:{1}' height='150'/></center><h3>Konfirmasi User Sales Kit Online Bank bjb</h3><p>Calon pengguna Aplikasi Sales Kit Online Bank <strong>bjb</strong> yang kami hormati,</p><p>Email Anda telah terdaftar di Aplikasi Sales Kit Online Bank <strong>bjb</strong>,</p><p>untuk menyelesaikan proses registrasi akun Anda silahkan untuk mengklik tautan berikut ini: </p><a href='{0}'>{0}</a><br/><p>Terima Kasih</p><p>PT Bank Pembangunan Daerah Jawa Barat dan Banten Tbk.</p></div></body>";
        public const string AccessDenied = "Maaf anda tidak ada akses untuk {0} data di halaman {1}";

        public const string CanView = "CanView";
        public const string CanAdd = "CanAdd";
        public const string CanEdit = "CanEdit";
        public const string CanDelete = "CanDelete";
        public const string CanApproveContentUnit = "CanApproveContentUnit";
        public const string CanApproveContentJLA = "CanApproveContentJLA";

        public const string MessageAction = "Data {0} berhasil {1}";
    }
}
