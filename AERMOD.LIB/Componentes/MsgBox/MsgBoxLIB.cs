using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using AERMOD.LIB.Componentes.MsgBox;

/// <summary>
/// MessageBox customizado({ Id = 0, Texto = "Nonde" } = None)
/// </summary>
public class MsgBoxLIB
{
    /// <summary>
    /// Get a imagem
    /// </summary>
    /// <param name="icon">MessageBoxIcon</param>
    /// <returns></returns>
    private static Bitmap GetImage(MessageBoxIcon icon)
    {
        Bitmap image = null;

        switch (icon.ToString())
        {
            case "Asterisk":
                image = SystemIcons.Asterisk.ToBitmap();
                break;
            case "Error":
                image = SystemIcons.Error.ToBitmap();
                break;
            case "Exclamation":
                image = SystemIcons.Exclamation.ToBitmap();
                break;
            case "Hand":
                image = SystemIcons.Hand.ToBitmap();
                break;
            case "Information":
                image = SystemIcons.Information.ToBitmap();
                break;
            case "None":
                image = null;
                break;
            case "Question":
                image = SystemIcons.Question.ToBitmap();
                break;
            case "Stop":
                image = SystemIcons.Shield.ToBitmap();
                break;
            case "Warning":
                image = SystemIcons.Warning.ToBitmap();
                break;
        }

        return image;
    }

    /// <summary>
    /// Displays a message box with specified text.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Texto = text;
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text.
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Texto = text;
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box with specified text and caption.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text and caption.
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, string caption)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box with specified text, caption, and icon.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption, MessageBoxIcon messageBoxIcon)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text, caption, and icon.
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, string caption, MessageBoxIcon messageBoxIcon)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        frmMessageBox.BoxButtons = new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "OK" } };
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    ///  Displays a message box with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="messageBoxButton">Array the buttons.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption, MessageBoxIcon messageBoxIcon, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    ///  Displays a message box with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="colorForm"> BackColor Form</param>
    /// <param name="colorButton">BackColor Button</param>
    /// <param name="messageBoxButton">Array the buttons.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption, MessageBoxIcon messageBoxIcon, Color colorForm, Color colorButton, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.BackColor = colorForm;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    ///  Displays a message box with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="chooseValue">Choose value</param>
    /// <param name="messageBoxButton">Array the buttons.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption, MessageBoxIcon messageBoxIcon, bool chooseValue, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ChooseValue = chooseValue;
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    ///  Displays a message box with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="chooseValue">Choose value</param>
    /// <param name="messageBoxButton">Array the buttons.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, string caption, MessageBoxIcon messageBoxIcon, bool chooseValue, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ChooseValue = chooseValue;
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="messageBoxButton">Array the buttons</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, string caption, MessageBoxIcon messageBoxIcon, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text, caption, icon and buttons(Custom).
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="textFont">Tipo da fonte do texto.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="messageBoxButton">Array the buttons</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, Font textFont, string caption, MessageBoxIcon messageBoxIcon, params MessageBoxButton[] messageBoxButton)
    {
        if (messageBoxButton.Any(f => f.Id == 0))
        {
            throw new Exception("O valor do identificar NÃO pode ser 0.");
        }

        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.FonteTexto = textFont;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        Array.Reverse(messageBoxButton);
        frmMessageBox.BoxButtons = messageBoxButton;
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box with the specified text, caption, icon and button.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="messageBoxButton">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(string text, string caption, MessageBoxIcon messageBoxIcon, MessageBoxButtons messageBoxButton)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        frmMessageBox.BoxButtons = GetButtons(messageBoxButton);
        frmMessageBox.ShowDialog();

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Displays a message box in front of the specified object and with the specified text, caption, icon and button.
    /// </summary>
    /// <param name="owner">An implementation of System.Windows.Forms.IWin32Window that will own the modal dialog box.</param>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The text to display in the title bar of the message box</param>
    /// <param name="messageBoxIcon"> One of the System.Windows.Forms.MessageBoxIcon values that specifies which icon to display in the message box.</param>
    /// <param name="messageBoxButton">One of the System.Windows.Forms.MessageBoxButtons values that specifies which buttons to display in the message box.</param>
    /// <returns></returns>
    public static MessageBoxButton Show(IWin32Window owner, string text, string caption, MessageBoxIcon messageBoxIcon, MessageBoxButtons messageBoxButton)
    {
        FrmMessageBox frmMessageBox = new FrmMessageBox();
        frmMessageBox.Owner = (Form)owner;
        frmMessageBox.StartPosition = FormStartPosition.CenterParent;
        frmMessageBox.Caption = caption;
        frmMessageBox.Texto = text;
        frmMessageBox.Imagem = GetImage(messageBoxIcon);
        frmMessageBox.BoxButtons = GetButtons(messageBoxButton);
        frmMessageBox.ShowDialog(owner);

        return frmMessageBox.GetMessageButton;
    }

    /// <summary>
    /// Obtem os MessageBoxButton atraves do MessageBoxButtons
    /// </summary>
    /// <param name="messageBoxButton"></param>
    /// <returns></returns>
    private static MessageBoxButton[] GetButtons(MessageBoxButtons messageBoxButton)
    {
        List<MessageBoxButton> listButton = new List<MessageBoxButton>();

        MessageBoxButton buttonOk = new MessageBoxButton();
        buttonOk.Id = 1;
        buttonOk.Texto = "OK";

        MessageBoxButton buttonCancel = new MessageBoxButton();
        buttonCancel.Id = 2;
        buttonCancel.Texto = "Cancelar";

        MessageBoxButton buttonYes = new MessageBoxButton();
        buttonYes.Id = 3;
        buttonYes.Texto = "Sim";

        MessageBoxButton buttonNo = new MessageBoxButton();
        buttonNo.Id = 4;
        buttonNo.Texto = "Não";

        MessageBoxButton buttonRetry = new MessageBoxButton();
        buttonRetry.Id = 5;
        buttonRetry.Texto = "Repetir";

        MessageBoxButton buttonAbort = new MessageBoxButton();
        buttonAbort.Id = 6;
        buttonAbort.Texto = "Abortar";

        MessageBoxButton buttonReIgnore = new MessageBoxButton();
        buttonReIgnore.Id = 7;
        buttonReIgnore.Texto = "Ignorar";

        switch (messageBoxButton)
        {
            case MessageBoxButtons.AbortRetryIgnore:
                {
                    listButton.Add(buttonReIgnore);
                    listButton.Add(buttonRetry);
                    listButton.Add(buttonAbort);
                }
                break;
            case MessageBoxButtons.OK:
                {
                    listButton.Add(buttonOk);
                }
                break;
            case MessageBoxButtons.OKCancel:
                {
                    listButton.Add(buttonCancel);
                    listButton.Add(buttonOk);
                }
                break;
            case MessageBoxButtons.RetryCancel:
                {
                    listButton.Add(buttonCancel);
                    listButton.Add(buttonRetry);
                }
                break;
            case MessageBoxButtons.YesNo:
                {
                    listButton.Add(buttonNo);
                    listButton.Add(buttonYes);
                }
                break;
            case MessageBoxButtons.YesNoCancel:
                {
                    listButton.Add(buttonCancel);
                    listButton.Add(buttonNo);
                    listButton.Add(buttonYes);
                }
                break;
        }

        return listButton.ToArray();
    }   
}

public class Win32Window : IWin32Window
{
    IntPtr handle;
    public Win32Window(IWin32Window window)
    {
        this.handle = window.Handle;
    }

    IntPtr IWin32Window.Handle
    {
        get { return handle; }
    }
}

public class MessageBoxButton
{
    public int Id { get; set; }
    public string Texto { get; set; }
    public bool HabilitaTimer { get; set; }
    public int TimerInicialDecremento { get; set; }
    /// <summary>
    /// Ativar atalho numérico utilizando o ID do botão.
    /// </summary>
    public bool AtalhoNumerico { get; set; }
}

