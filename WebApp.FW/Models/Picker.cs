using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.FW.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Picker
    {
        public string Text { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public bool Selected { get; set; } = false;

        public bool Disabled { get; set; } = false;

        ///// <summary>
        ///// Cast Netcenter User to a Picker model
        ///// </summary>
        ///// <param name="user"></param>
        //public static explicit operator Picker(PORTS.Netcenter.Dto.UserDto user)
        //{
        //    return new Picker
        //    {
        //        title = null,
        //        disabled = false,
        //        text = user.Fullname,
        //        value = user.Badge,
        //        selected = true // pre-select for the multi-picker component
        //    };
        //}
    }
}