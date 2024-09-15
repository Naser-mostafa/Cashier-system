using Cafe.Properties;
using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Cafe
{
    public partial class FrmAddEditDrink : Form
    {
        public FrmAddEditDrink(int ID,FoodOrDrink FD)
        {
            FoodsOrDrinks = FD;
           
            InitializeComponent();
            if (ID == -1&&FD==FoodOrDrink.Drink)
            {
                HideFoodDetails();
                CurrentMode = FrmAddEditDrink.Mode.AddNew;
                pictureBox1.Image = Resources.images;
                SetUpFormToAdding();
            }
            else if(ID==-1&&FD==FoodOrDrink.Food)
            {
                ShowFoodDetails();
                CurrentMode = FrmAddEditDrink.Mode.AddNew;
                SetUpFormToAdding();
            }

            else if(FD==FoodOrDrink.Drink)
            {
                HideFoodDetails();
                CurrentMode = FrmAddEditDrink.Mode.Update;
                SetUpFormToUpdate();
                drinkDetails = ClsDrinks.GetDrinkById(ID);
                SetInfoIntoDetailsIntoUi(ID);
            }
            else
            {
                ShowFoodDetails();
                CurrentMode = FrmAddEditDrink.Mode.Update;
                SetUpFormToUpdate();
                FoodDetails = ClsDrinks.GetFoodById(ID);
                SetFoodInfoIntoDetailsIntoUi(ID);

            }
        }
     public enum FoodOrDrink
        {
            Food,Drink
        }
        private void HideFoodDetails()
        {
            TxMidPrice.Visible = false;
            LbMid.Visible = false;
            LbLarge.Text = "السعر";
            TxSmallPrice.Visible = false;
           LbSmall.Visible = false;
        }
        private void ShowFoodDetails()
        {
            TxMidPrice.Visible = true;
            TxSmallPrice.Visible = true;
            LbLarge.Text = "السعر للكبير";

            LbMid.Visible = true;
            LbSmall.Visible = true;
        }
        public FoodOrDrink FoodsOrDrinks;

        public DrinkDetails drinkDetails =null;
        public FoodDetails FoodDetails = null;

        private void SetFoodInfoIntoDetailsIntoUi(int Id)
        {
            this.TxName.Text = FoodDetails.DrinkName;
            this.TxLargePrice.Text = FoodDetails.Price.ToString();
            pictureBox1.ImageLocation = FoodDetails.PicturePath;
            ImagePath = FoodDetails.PicturePath;
            TxMidPrice.Text = FoodDetails.MidPrice.ToString();
            TxSmallPrice.Text = FoodDetails.SmallPrice.ToString();
        }
        private void SetInfoIntoDetailsIntoUi(int Id)
        {
            this.TxName.Text = drinkDetails.DrinkName;
            this.TxLargePrice.Text = drinkDetails.Price.ToString();
            pictureBox1.ImageLocation = drinkDetails.PicturePath;
            ImagePath = drinkDetails.PicturePath;
        }

        private void GetInfoFromUi()
        {
            drinkDetails.DrinkName = this.TxName.Text;
            drinkDetails.Price = float.Parse(this.TxLargePrice.Text);
          
        }
      
        private void GetFoodInfoFromUi()
        {
            FoodDetails.DrinkName = this.TxName.Text;
            FoodDetails.Price = float.Parse(this.TxLargePrice.Text);
            FoodDetails.MidPrice = float.Parse(TxMidPrice.Text);
            FoodDetails.SmallPrice = float.Parse(TxSmallPrice.Text);
            FoodDetails.PicturePath = ImagePath;

        }

        private void SetUpFormToAdding()
        {
            LbFormAddress.Text = "اضافه عنصر";
            LkAddPicture.Text = "اضافه الصوره";
        }
        private void SetUpFormToUpdate()
        {
            LbFormAddress.Text = "تعديل العنصر";
            LkAddPicture.Text = "تغير الصوره";
        }
        Mode CurrentMode;
        enum Mode
        {
            AddNew, Update
        }
        string ImagePath = "";
        private bool IsInformationIsValidAndConsistant()
        {
           
            if (!string.IsNullOrEmpty(TxName.Text))
            {
                return (float.TryParse(TxLargePrice.Text, out float Price));
            }
            return false;
        }
        private bool IsInformationIsValidAndConsistantForFoods()
        {

            if (!string.IsNullOrEmpty(TxName.Text))
            {
                return (float.TryParse(TxLargePrice.Text, out float Price)&& float.TryParse(TxMidPrice.Text, out float MidPrice)&& float.TryParse(TxSmallPrice.Text, out float SmallPrice));
            }
            return false;
        }
        private void AddNew()
        {

            if (IsInformationIsValidAndConsistant())
            {
                if (ClsDrinks.AddNew(TxName.Text, float.Parse(TxLargePrice.Text), ImagePath))
                {
                    ClsSettings.ShowMessagboxForSuccessAdding();
                    this.Close();
                }
                
                else
                    ClsSettings.ShowMessagboxForFalireOperations();
                this.Close();
            }
            else
            {
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
                this.Close();
            }
        }
        private void AddNewFood()
        {

            if (IsInformationIsValidAndConsistantForFoods())
            {
                if (ClsDrinks.AddNewFood(TxName.Text, float.Parse(TxLargePrice.Text), float.Parse(TxSmallPrice.Text), float.Parse(TxMidPrice.Text), ImagePath))
                {
                    ClsSettings.ShowMessagboxForSuccessAdding();
                    this.Close();
                }

                else
                    ClsSettings.ShowMessagboxForFalireOperations();
                this.Close();
            }
            else
            {
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
                this.Close();
            }
        }
        private new void Update()
        {
            if (IsInformationIsValidAndConsistant())
            {
                
                GetInfoFromUi();
                if (ClsDrinks.UpdateDrink(drinkDetails.ID, drinkDetails.DrinkName, drinkDetails.Price,ImagePath ))
                {
                    ClsSettings.ShowMessagboxForSuccessUPdating();
                    this.Close();
                }
                else
                {
                    ClsSettings.ShowMessagboxForFalireOperations();
                    this.Close();
                }
            }
            else
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
            this.Close();
        }
        private  void UpdateForFood()
        {
            if (IsInformationIsValidAndConsistantForFoods())
            {
                
                GetFoodInfoFromUi();
                if (ClsDrinks.UpdateFood(FoodDetails.ID, FoodDetails.DrinkName, (FoodDetails.Price), float.Parse(TxSmallPrice.Text),float.Parse( TxMidPrice.Text), ImagePath))
                {
                    ClsSettings.ShowMessagboxForSuccessUPdating();
                    this.Close();
                }
                else
                {
                    ClsSettings.ShowMessagboxForFalireOperations();
                    this.Close();
                }
            }
            else
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
            this.Close();
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            if (FoodsOrDrinks == FoodOrDrink.Drink) {
                switch (CurrentMode)
                {
                    case Mode.AddNew:
                        {
                            AddNew();

                        }
                        break;
                    case Mode.Update:
                        {
                            Update();
                        }
                        break;
                }

            }
            else
            {
                switch (CurrentMode)
                {
                    case Mode.AddNew:
                        {
                            AddNewFood();

                        }
                        break;
                    case Mode.Update:
                        {
                            UpdateForFood();
                        }
                        break;
                }
            }
        }

        private void FrmAddEditDrink_Load(object sender, EventArgs e)
        {

        }

        private void LkAddPicture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر صورة";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            // فتح نافذة الاختيار والتحقق من اختيار المستخدم لملف
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // تحميل الصورة وعرضها في PictureBox
                ImagePath = openFileDialog.FileName;
                Image selectedImage = Image.FromFile(ImagePath);
                pictureBox1.Image = selectedImage;
            }
        }
    }
}
