﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain
{
    public class DBException : Exception
    {
        /// <summary>
        /// Mã lỗi cho ngoại lệ
        /// </summary>

        public int ErrorCode { get; set; }
        /// <summary>
        /// Constructor mặc định
        /// </summary>
        public DBException() { }
        /// <summary>
        /// Constructor với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public DBException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo và mã lỗi
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        /// <param name="errorCode">Mã lỗi</param>
        public DBException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// Constructor với thông báo
        /// </summary>
        /// <param name="message">Thông báo ngoại lệ</param>
        public DBException(string message) : base(message)
        {

        }
    }
}
