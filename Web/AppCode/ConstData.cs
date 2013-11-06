using System;
using System.Collections.Generic;
using System.Web;
using freePhoto.Tools;

namespace freePhoto.Web.AppCode
{
    public static class ConstData
    {
        /// <summary>
        /// 照片纸
        /// </summary>
        public static decimal PhotoPaper
        {
            get
            {
                decimal _PhotoPaper = 0;
                if(decimal.TryParse(fun.getapp("PhotoPaper"), out _PhotoPaper)) return _PhotoPaper;
                return 0.7M;
            }
        }
        /// <summary>
        /// 普通纸
        /// </summary>
        public static decimal NormalPaper
        {
            get
            {
                decimal _NormalPaper = 0;
                if(decimal.TryParse(fun.getapp("NormalPaper"), out _NormalPaper)) return _NormalPaper;
                return 0.1M;
            }
        }
        /// <summary>
        /// 普通黑白纸
        /// </summary>
        public static decimal PTHBPaper
        {
            get
            {
                decimal _PTHBPaper = 0;
                if(decimal.TryParse(fun.getapp("PTHBPaper"), out _PTHBPaper)) return _PTHBPaper;
                return 0.42M;
            }
        }
        /// <summary>
        /// 普通彩色纸
        /// </summary>
        public static decimal PTCSPaper
        {
            get
            {
                decimal _PTCSPaper = 0;
                if(decimal.TryParse(fun.getapp("PTCSPaper"), out _PTCSPaper)) return _PTCSPaper;
                return 0.52M;
            }
        }
        /// <summary>
        /// 相片彩色纸
        /// </summary>
        public static decimal XPCSPaper
        {
            get
            {
                decimal _XPCSPaper = 0;
                if(decimal.TryParse(fun.getapp("XPCSPaper"), out _XPCSPaper)) return _XPCSPaper;
                return 0.72M;
            }
        }

        /// <summary>
        /// 连续登录7天赠送免费普通纸数量
        /// </summary>
        public static int Donate_Login7
        {
            get
            {
                int num = 0;
                if(Int32.TryParse(fun.getapp("Donate_Login7"), out num)) return num;
                return 12;
            }
        }

        /// <summary>
        /// 连续登录3天赠送免费普通纸数量
        /// </summary>
        public static int Donate_Login3
        {
            get
            {
                int num = 0;
                if(Int32.TryParse(fun.getapp("Donate_Login3"), out num)) return num;
                return 5;
            }
        }

        /// <summary>
        /// 每天登录的基础赠送免费普通纸数量
        /// </summary>
        public static int Donate_Login
        {
            get
            {
                int num = 0;
                if(Int32.TryParse(fun.getapp("Donate_Login"), out num)) return num;
                return 8;
            }
        }

        /// <summary>
        /// 登记个人信息，每项赠送照片纸数量
        /// </summary>
        public static int Donate_SetInfo
        {
            get
            {
                int num = 0;
                if(Int32.TryParse(fun.getapp("Donate_SetInfo"), out num)) return num;
                return 4;
            }
        }

        /// <summary>
        /// 每日最多使用免费照片纸数量
        /// </summary>
        public static int UseFreePhotoCount
        {
            get
            {
                int num = 0;
                if(Int32.TryParse(fun.getapp("UseFreePhotoCount"), out num)) return num;
                return 4;
            }
        }
    }
}