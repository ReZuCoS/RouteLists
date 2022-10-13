using RouteLists.Model;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditVehicle : EntityPage
    {
        private Vehicle _vehicle = null;

        public PageEditVehicle()
        {
            this.Title = "Добавление авто";
            InitializeComponent();
        }

        public PageEditVehicle(Vehicle vehicle)
        {
            this.Title = "Изменение авто";
            InitializeComponent();

            this._vehicle = vehicle;
            txtBoxBrand.Text = vehicle.Brand;
            txtBoxNumber.Text = vehicle.Number;
            txtBoxNumber.IsEnabled = false;
            txtBoxTonnage.Text = vehicle.Tonnage.ToString();

            if (vehicle.HasVehiclePass && vehicle.VehiclePass.PassTypeID != 0)
            {
                checkBoxHasPass.IsChecked = true;
                cBoxPassType.SelectedIndex = vehicle.VehiclePass.PassTypeID - 1;
                datePickerPassExpire.SelectedDate = vehicle.VehiclePass.ExpireDate;
            }
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит маршрутные листы, в которых встречается данное авто.\n\n" +
                $"Вы действительно хотите удалить автомобиль: {_vehicle.Number}?\n\nДанное действие невозможно отменить!",
                "Удаление автомобиля", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                List<VehiclePass> vehiclePasses = DatabaseContext.Database.VehiclePasses.ToList().Where(vp => vp.VehicleID == _vehicle.ID).ToList();
                DatabaseContext.Database.VehiclePasses.RemoveRange(vehiclePasses);

                List<RouteList> routeLists = DatabaseContext.Database.RouteLists.ToList().Where(rl => rl.VehicleID == _vehicle.ID).ToList();
                DatabaseContext.Database.RouteLists.RemoveRange(routeLists);

                DatabaseContext.Database.Vehicles.Remove(_vehicle);
                DatabaseContext.SaveDatabase();
                return true;
            }

            return false;
        }

        public override bool EntitySaved()
        {
            if (!EntityValidated())
            {
                return false;
            }

            if (_vehicle == null)
            {
                _vehicle = new Vehicle();
                DatabaseContext.Database.Vehicles.Add(_vehicle);
            }

            AppendVehicleData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private void AppendVehicleData()
        {
            _vehicle.Number = Vehicle.VehicleNumberFromString(txtBoxNumber.Text);
            _vehicle.Brand = txtBoxBrand.Text;
            _vehicle.Tonnage = int.Parse(txtBoxTonnage.Text);

            if (_vehicle.Tonnage >= 1000)
            {
                if (checkBoxHasPass.IsChecked.Value)
                {
                    _vehicle.VehiclePass = new VehiclePass()
                    {
                        PassTypeID = cBoxPassType.SelectedIndex + 1,
                        ExpireDate = datePickerPassExpire.SelectedDate.Value
                    };
                }
                else
                {
                    _vehicle.VehiclePass = new VehiclePass()
                    {
                        PassTypeID = 0,
                        ExpireDate = DateTime.Now
                    };
                }

                DatabaseContext.Database.VehiclePasses.Add(_vehicle.VehiclePass);
            }
        }

        private bool EntityValidated()
        {
            if (txtBoxBrand.Text.Replace(" ", "") == "" ||
                txtBoxTonnage.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите все требуемые значения!");
                return false;
            }

            if (!StringValidator.IsCorrectVehicleNumber(txtBoxNumber.Text))
            {
                MessageBox.Show("Автомобильный номер введён некорректно!");
                return false;
            }

            if (!StringValidator.IsDigitsOnly(txtBoxTonnage.Text))
            {
                MessageBox.Show("Тоннаж должен содержать только цифры!");
                return false;
            }

            if (checkBoxHasPass.IsChecked.Value)
            {
                if (cBoxPassType.SelectedIndex < 0)
                {
                    MessageBox.Show("Выберите тип пропуска!");
                    return false;
                }

                if (datePickerPassExpire.SelectedDate <= DateTime.Now || datePickerPassExpire.SelectedDate == null)
                {
                    MessageBox.Show("Дата истечения пропуска не может быть прошедшей или нынешней!");
                    return false;
                }
            }

            if (_vehicle == null && !Vehicle.IsUniqueNumber(Vehicle.VehicleNumberFromString(txtBoxNumber.Text)))
            {
                MessageBox.Show("Данный гос. номер уже зарегестрирован в системе!");
                return false;
            }

            return true;
        }
    }
}
