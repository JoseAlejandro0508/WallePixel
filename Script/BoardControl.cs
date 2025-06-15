using Godot;
using System;
using System.Diagnostics;


public partial class BoardControl: Sprite2D
{
    public int BoardSize;

    private Image _image;
    private ImageTexture _texture;

    public override void _Ready()
    {
        CreateBoard();

    }
    public void CreateBoard(){
        GlobalDates.InitBoardMatrix();

        BoardSize=GlobalDates.BoardSize.x;
        // Crear imagen y textura
        _image = Image.Create(BoardSize, BoardSize, false, Image.Format.Rgba8);
        _image.Fill(GlobalDates.ColorsDictionary[GlobalDates.BoardColor]);
        _texture = ImageTexture.CreateFromImage(_image);
        //_texture._Set("flags", 0);

        Texture = _texture;
        TextureFilter = TextureFilterEnum.Nearest;


        // Configuración de visualización
        Centered = false;
        Position = (new Vector2(0, 0));
    }
    public void AutoScale()
    {
        var viewport = GetViewport();
        var screenSize = viewport.GetVisibleRect().Size;
        Vector2 scale_Adjust = new Vector2(screenSize.X / (2 * BoardSize), screenSize.Y / BoardSize);
         // Vector2 scale_Adjust = new Vector2(screenSize.X / (2 * BoardSize), screenSize.X / (2 * BoardSize));
        Scale = scale_Adjust;

    }
    // Método para establecer un píxel
    public void SetPixel(int x, int y, string color_)
    {
        if(color_=="Transparent")return;
        Color color=GlobalDates.ColorsDictionary[color_];
        if (!IsPositionValid(x, y))
        {
            return;

    
        
        }
        int BrushSize=(GlobalDates.BrushSize-1)/2;

        for(int x_=0;x_<GlobalDates.BoardSize.x;x_++){
            for(int y_=0;y_<GlobalDates.BoardSize.x;y_++){
                if(Math.Abs(x_-x)<=BrushSize&&Math.Abs(y_-y)<=BrushSize ){
                    _image.SetPixel(x_, y_, color);
                    GlobalDates.BoardMatrix[x_,y_]=color_;

                }
            }
        }

        UpdateBoard();
    }

    // Método para actualizar la textura
    public void UpdateBoard()
    {
        _texture.Update(_image);
    }

    // Método para limpiar el tablero
    public void ClearBoard()
    {
        _image.Fill(Colors.White);
        UpdateBoard();
    }

    private bool IsPositionValid(int x, int y)
    {
        return x >= 0 && x < BoardSize && y >= 0 && y < BoardSize;
    }
    public override void _Process(double delta)
    {

        AutoScale();

    }
}
