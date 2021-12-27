using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P79.Application
{
    public class Constant
    {
        public const string TOKEN_AUTHORIZATION_SCHEME = "Bearer";
        public const int TOKEN_EXPIRED = 300;//expired in minutes

        public const string APPLICATION_NAME = "X-APP";

        public const string NAMA_APLIKASI = "P79";
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
        #region Garansi Bank
        [Display(Name = "Customers")]
        public const string MODULE_CUSTOMERS = "Customers";
        [Display(Name = "DataMaster")]
        public const string MODULE_DATA_MASTER = "Data Master";
        [Display(Name = "Monitoring")]
        public const string MODULE_MONITORING = "Monitoring";
        [Display(Name = "Reporting")]
        public const string MODULE_REPORTING = "Laporan";
        [Display(Name = "RoleAccess")]
        public const string MODULE_ROLEACCESS = "Manajemen Hak Akses";

        [Display(Name = "ApiClient")]
        public const string FEATURE_API_CLIENT = "ApiClient";
        [Display(Name = "BankGuarenteeType")]
        public const string FEATURE_BANK_GUARANTEE_TYPE = "BankGuaranteeType";
        [Display(Name = "BankGuarentee")]
        public const string FEATURE_BANK_GUARANTEE = "BankGuarantee";
        [Display(Name = "Adendum")]
        public const string ADENDUM = "Adendum";
        [Display(Name = "Branch")]
        public const string FEATURE_BRANCH = "Branch";
        [Display(Name = "BusinessField")]
        public const string FEATURE_BUSINESS_FIELD = "BusinessField";
        [Display(Name = "BusinessType")]
        public const string FEATURE_BUSINESS_TYPE = "BusinessType";
        [Display(Name = "ClaimPeriod")]
        public const string FEATURE_CLAIM_PERIOD = "ClaimPeriod";
        [Display(Name = "Currency")]
        public const string FEATURE_CURRENCY = "Currency";
        [Display(Name = "City")]
        public const string FEATURE_CITY = "City";
        [Display(Name = "CollateralType")]
        public const string FEATURE_COLLATERAL_TYPE = "CollateralType";
        [Display(Name = "Customer")]
        public const string FEATURE_CUSTOMER = "Customer";
        [Display(Name = "CustomerUser")]
        public const string FEATURE_CUSTOMER_USER = "CustomerUser";
        [Display(Name = "EconomicSector")]
        public const string FEATURE_ECONOMIC_SECTOR = "EconomicSector";
        [Display(Name = "GuaranteeBanksForm")]
        public const string FEATURE_GUARANTEE_BANK_FORM = "GuaranteeBanksForm";
        [Display(Name = "Language")]
        public const string FEATURE_LANGUAGE = "Language";
        [Display(Name = "LegalEntity")]
        public const string FEATURE_LEGAL_ENTITY = "LegalEntity";
        [Display(Name = "PasswordPolicy")]
        public const string FEATURE_PASSWORD_POLICY = "PasswordPolicy";
        [Display(Name = "Province")]
        public const string FEATURE_PROVINCE = "Province";
        [Display(Name = "Retention")]
        public const string FEATURE_RETENTION = "Retention";
        [Display(Name = "User")]
        public const string FEATURE_USERS = "User";
        [Display(Name = "Role")]
        public const string FEATURE_ROLES = "Role";
        [Display(Name = "CanView")]
        public const string CanView = "Menampilkan";
        [Display(Name = "CanAdd")]
        public const string CanAdd = "Menambahkan";
        [Display(Name = "CanEdit")]
        public const string CanEdit = "Mengubah";
        [Display(Name = "CanDelete")]
        public const string CanDelete = "Menghapus";
        [Display(Name = "CanApprove")]
        public const string CanApproveJLA = "Approval JLA";
        [Display(Name = "CanAssign")]
        public const string CanApproveDivision = "Approval Division";
        #endregion


        public const string FEATURE_KONTEN = "Konten";
        public const string FEATURE_ALUR_KONTEN = "Alur Konten";
        public const string FEATURE_UNIT_KERJA = "Unit Kerja";
        
        public const string FEATRURE_BANK_SOAL = "Bank Soal";
        public const string EMAIL_VERIFICATION = "<body><div class = 'cnk-line-wrapper'><center><img src='cid:{1}' height='150'/></center><h3>Konfirmasi User Garansi Bank Bank bjb</h3><p>Calon pengguna Aplikasi Garansi Bank <strong>bjb</strong> yang kami hormati,</p><p>Email Anda telah terdaftar di Aplikasi Garansi Bank <strong>bjb</strong>,</p><p>untuk menyelesaikan proses registrasi akun Anda silahkan untuk mengklik tautan berikut ini: </p><a href='{0}'>{0}</a><br/><p>Terima Kasih</p><p>PT Bank Pembangunan Daerah Jawa Barat dan Banten Tbk.</p></div></body>";
        public const string EMAIL_RESET_PASSWORD = "<body><div class = 'cnk-line-wrapper'><center><img src='cid:{1}' height='150'/></center><h3>Reset Password Garansi Bank Online Bank bjb</h3><p>Untuk me-reset password akun Anda silahkan untuk mengklik tautan berikut ini:</p> <a href='{0}'>{0}</a><br/><p>Terima Kasih</p><p>PT Bank Pembangunan Daerah Jawa Barat dan Banten Tbk.</p></div></body>";
        public const string AccessDenied = "Maaf anda tidak ada akses untuk {0} data di halaman {1}";

        

        public const string MessageAction = "Data {0} berhasil {1}";

    }
}
