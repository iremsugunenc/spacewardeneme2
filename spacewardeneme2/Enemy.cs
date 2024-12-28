public abstract class Enemy
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Health { get; set; }
    public int Speed { get; set; }
    public PictureBox PictureBox { get; set; }
    public int Damage { get; set; }
   
    public Enemy(int x, int y, int health, int speed, int damage)
    {
        X = x;
        Y = y;
        Health = health;
        Speed = speed;
        Damage = damage;
    }

    // Düşman boyutları için Bounds özelliği ekleniyor
    public Rectangle Bounds
    {
        get
        {
            return new Rectangle(X, Y, PictureBox.Width, PictureBox.Height);
        }
    }

    public virtual Image GetImage()
    {
        return null; // Temel sınıf resim döndürmez
    }

    // Her düşman türü kendi hareket metodunu belirler
    public abstract void Move(int playerX, int playerY);

    public void UpdatePictureBoxLocation()
    {
        if (PictureBox != null)
        {
            PictureBox.Location = new Point(X, Y);
        }
    }

    // Düşman mermisi yaratma
    public abstract Bullet Shoot();

    // Düşmana hasar verme
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
