﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfzoo.entities;
using wpfzoo.entities.enums;

namespace wpfzoo.views
{
    /// <summary>
    /// Logique d'interaction pour UserControlLayoutView.xaml
    /// </summary>
    public partial class UserControlLayoutView : Page
    {
        public UserControlLayoutView()
        {
            InitializeComponent();
            Animal animal = new Animal();
            animal.Name = "moufassa";
            animal.Gender = Gender.MALE;
            animal.Birth = DateTime.Now;
            animal.Height = 1.2M;
            animal.Weight = 120;
            animal.IsDead = false;
            this.animalUC.Animal = animal;
        }
    }
}
