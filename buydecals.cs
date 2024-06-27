using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data;
using System.Windows.Forms;

namespace kursSmol
{
    public partial class buydecals : MaterialForm
    {
        private MaterialSingleLineTextField textBoxNameDec;
        private MaterialSingleLineTextField textBoxGodVipDec;
        private MaterialSingleLineTextField textBoxFirma;
        private MaterialSingleLineTextField textBoxCountryDec;
        private MaterialSingleLineTextField textBoxQuantity;
        private MaterialRaisedButton buttonCreateOrder;

        public buydecals()
        {
            InitializeComponent();
            InitializeMaterialComponents();
        }

        private void InitializeMaterialComponents()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700,
                Primary.Blue200, Accent.LightBlue200,
                TextShade.WHITE
            );

            textBoxNameDec = new MaterialSingleLineTextField
            {
                Hint = "Название декали",
                Location = new System.Drawing.Point(20, 80),
                Width = 260
            };

            textBoxGodVipDec = new MaterialSingleLineTextField
            {
                Hint = "Год выпуска декали",
                Location = new System.Drawing.Point(20, 130),
                Width = 260
            };
            textBoxGodVipDec.KeyPress += TextBoxGodVipDec_KeyPress;

            textBoxFirma = new MaterialSingleLineTextField
            {
                Hint = "Фирма",
                Location = new System.Drawing.Point(20, 180),
                Width = 260
            };

            textBoxCountryDec = new MaterialSingleLineTextField
            {
                Hint = "Страна декали",
                Location = new System.Drawing.Point(20, 230),
                Width = 260
            };

            textBoxQuantity = new MaterialSingleLineTextField
            {
                Hint = "Количество",
                Location = new System.Drawing.Point(20, 280),
                Width = 260
            };

            buttonCreateOrder = new MaterialRaisedButton
            {
                Text = "Создать заказ",
                Location = new System.Drawing.Point(20, 330),
                Width = 260
            };
            buttonCreateOrder.Click += buttonCreateOrder_Click;

            Controls.Add(textBoxNameDec);
            Controls.Add(textBoxGodVipDec);
            Controls.Add(textBoxFirma);
            Controls.Add(textBoxCountryDec);
            Controls.Add(textBoxQuantity);
            Controls.Add(buttonCreateOrder);
        }

        private async void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            string nameDec = textBoxNameDec.Text;
            int godVipDec;
            if (!int.TryParse(textBoxGodVipDec.Text, out godVipDec))
            {
                MessageBox.Show("Пожалуйста, введите корректный год выпуска декали.");
                return;
            }

            string firma = textBoxFirma.Text;
            string countryDec = textBoxCountryDec.Text;
            int quantity;
            if (!int.TryParse(textBoxQuantity.Text, out quantity))
            {
                MessageBox.Show("Пожалуйста, введите корректное количество декалей.");
                return;
            }

            try
            {
                await MySQL.QueryAsync($"INSERT INTO `purchase_orders` (`namedec`, `godvipdec`, `firma`, `countrydec`, `quantity`, `order_date`) VALUES ('{nameDec}', {godVipDec}, '{firma}', '{countryDec}', {quantity}, '{DateTime.Now}')");
                MessageBox.Show("Закупка успешно создана.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании закупки: " + ex.Message);
            }
        }

        private void TextBoxGodVipDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
