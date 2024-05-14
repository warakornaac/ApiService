﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiService.Models
{
    public class SmsModels
    {
        public string Phone { get; set; }
        public string Text { get; set; }
        public string User { get; set; }
    }
    public class SendDeliveryModels
    {
        public string Uid { get; set; }
        public string Docno { get; set; }
        public string Docdate { get; set; }
        public string Cusname { get; set; }
        public string Delivery { get; set; }
        public string User { get; set; }
    }
    public class SmsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class SmsApiResponse
    {
        public int RemainingCredit { get; set; }
        public int TotalUseCredit { get; set; }
        public string CreditType { get; set; }
        public PhoneNumberListItem[] PhoneNumberList { get; set; }
        public object[] BadPhoneNumberList { get; set; }
    }
    public class PhoneNumberListItem
    {
        public string Number { get; set; }
        public string MessageId { get; set; }
        public int UsedCredit { get; set; }
    }
}