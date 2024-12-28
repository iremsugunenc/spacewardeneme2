using spacewardeneme2;

public class Bullet
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int Speed { get; set; }
    public Direction Direction { get; set; }
    public int Damage { get; set; }
    public bool IsVisible { get; set; }
    public bool IsEnemy { get; set; } // Merminin düşmana mı ait olduğunu gösterir
    public PictureBox PictureBox { get; set; } 

    // Merminin Bounds'ını döndüren özellik
    public Rectangle Bounds
    {
        get
        {
            return new Rectangle(PositionX, PositionY, PictureBox.Width, PictureBox.Height);
        }
    }

    // Oyuncu mermisi için yapıcı metot
    public Bullet(int x, int y, int speed, Direction direction, int damage)
    {
        PositionX = x;
        PositionY = y;
        Speed = speed;
        Direction = direction;
        Damage = damage;
        IsVisible = true;  // Başlangıçta mermi görünür
        IsEnemy = false;  // mermi oyuncuya ait
    }

    // Düşman mermisi için özel yapıcı metot
    public Bullet(int x, int y, int speed, Direction direction, int damage, bool isEnemy)
    {
        PositionX = x;
        PositionY = y;
        Speed = speed;
        Direction = direction;
        Damage = damage;
        IsVisible = true; // Başlangıçta mermi görünür
        IsEnemy = isEnemy; // Mermi düşmanın
    }

    // Merminin hareket etmesini sağlar
    public void Move()
    {
        int directionValue = Direction == Direction.Right ? 1 : Direction == Direction.Left ? -1 : 0;
        PositionX += directionValue * Speed; // Yalnızca yatay hareket eder
    }

    // Çarpışma sonrası mermiyi yok et
    public void OnHit()
    {
        IsVisible = false; // Mermiyi görünmez yap
    }
}