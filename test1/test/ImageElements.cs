﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Microsoft.Win32;
using System.Reflection;

namespace test
{
    public partial class MainWindow : Window
    {
        #region Background

        private void BtnOpenFile_back_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obraz JPG |*.jpg;*.jpeg|" +
                        "Obraz PNG |*.png|" +
                        "Obraz TIFF |*.tif|" +
                        "Plik PDF |*.pdf|" +
                        "Wszystkie pliki |*.png;*.jpg;*.jpeg;*.tif;*pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                LoadBackgroundImage(fileName);
            }
        }

        private void LoadBackgroundImage(string imageName)
        {
            if (imageName == null)
            {
                back.Source = new BitmapImage(new Uri(@"pack://application:,,,/Pictures/back0.png"));
            }
            else
            {
                if (Uri.TryCreate(imageName, UriKind.Absolute, out Uri imageUri))
                {   // Treat imageName as URI
                    back.Source = new BitmapImage(imageUri);
                }
                else
                {   // Treat imageName as a resource image
                    back.Source = new BitmapImage(new Uri($@"pack://application:,,,/Pictures/{imageName}.png"));
                }
            }
        }

        #endregion

        #region Foreground / Frame

        private void BtnOpenFile_frame_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obraz JPG |*.jpg;*.jpeg|" +
                        "Obraz PNG |*.png|" +
                        "Obraz TIFF |*.tif|" +
                        "Plik PDF |*.pdf|" +
                        "Wszystkie pliki |*.png;*.jpg;*.jpeg;*.tif;*pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                LoadFrameImage(fileName);
            }
        }

        private void LoadFrameImage(string imageName)
        {
            if (imageName == null)
            {
                frame.Source = null;
            }
            else
            {
                if (Uri.TryCreate(imageName, UriKind.Absolute, out Uri imageUri))
                {   // Treat imageName as URI
                    frame.Source = new BitmapImage(imageUri);
                }
                else
                {   // Treat imageName as a resource image
                    frame.Source = new BitmapImage(new Uri($@"pack://application:,,,/Frames/{imageName}.png"));
                }
            }
        }

        #endregion

        #region Elements

        private void BtnOpenFile_elem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Obraz JPG |*.jpg;*.jpeg|" +
                        "Obraz PNG |*.png|" +
                        "Obraz TIFF |*.tif|" +
                        "Plik PDF |*.pdf|" +
                        "Wszystkie pliki |*.png;*.jpg;*.jpeg;*.tif;*pdf"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                CreateElement(fileName);
            }
        }
        private Image CreateElement(string imageName)
        {
            BitmapImage tempBitmap;

            if (Uri.TryCreate(imageName, UriKind.Absolute, out Uri imageUri))
            {   // Treat imageName as URI
                tempBitmap = new BitmapImage(imageUri);
            }
            else
            {   // Treat imageName as a resource image
                tempBitmap = new BitmapImage(new Uri($@"pack://application:,,,/Elements/{imageName}.png"));
            }

            Image newElement = new Image();

            newElement.MouseUp += OnMouseUp;
            newElement.MouseDown += OnMouseDown;
            newElement.MouseMove += OnMouseDrag;
            newElement.MouseWheel += OnMouseScroll;

            newElement.Source = tempBitmap;

            TransformGroup newTransformGroup = new TransformGroup();
            newTransformGroup.Children.Add(new RotateTransform());
            newTransformGroup.Children.Add(new ScaleTransform());
            newTransformGroup.Children.Add(new TranslateTransform());

            newElement.RenderTransform = newTransformGroup;
            newElement.RenderTransformOrigin = new Point(0.5, 0.5);

            Size canvasSize = new Size(500, 500);
            if (tempBitmap.Width > canvasSize.Width || tempBitmap.Height > canvasSize.Height)
            {
                float fitScale = Math.Min((float)(canvasSize.Width / tempBitmap.Width), (float)(canvasSize.Height / tempBitmap.Height));

                newElement.Width = tempBitmap.Width * fitScale;
                newElement.Height = tempBitmap.Height * fitScale;
            }

            imageSpace.Children.Add(newElement);

            return newElement;
        }

        #endregion

        #region Text

        TextBlock SelectedTextBlock = null;            

        private void BtnCreateTextBox_Click(object sender, RoutedEventArgs e)
        {
            // Tworzenie nowego pola tekstowego
            TextBlock newText = new TextBlock();

            newText.MouseUp += OnMouseUp;
            newText.MouseDown += OnMouseDownTextBlock;
            newText.MouseMove += OnMouseDrag;
            newText.MouseWheel += OnMouseScroll;

            newText.Text = "Przykładowy nowy tekst";

            newText.FontSize = 22;
            newText.FontFamily = textBlock.FontFamily;
            newText.Foreground = textBlock.Foreground;
            newText.TextWrapping = TextWrapping.Wrap;
            newText.TextAlignment = TextAlignment.Center;
            newText.MaxWidth = 256;

            TransformGroup newTransformGroup = new TransformGroup();
            newTransformGroup.Children.Add(new RotateTransform());
            newTransformGroup.Children.Add(new ScaleTransform());
            newTransformGroup.Children.Add(new TranslateTransform());

            newText.RenderTransform = newTransformGroup;
            newText.RenderTransformOrigin = new Point(0.5, 0.5);
            newText.Margin = new Thickness(100, 100, 0, 0);

            imageSpace.Children.Add(newText);
        }
        
        #endregion
    }
}
