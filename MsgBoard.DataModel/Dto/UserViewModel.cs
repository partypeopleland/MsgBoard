﻿using System.ComponentModel.DataAnnotations;

namespace MsgBoard.DataModel.Dto
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string Pic { get; set; }

        public string Guid { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsDel { get; set; }
    }
}