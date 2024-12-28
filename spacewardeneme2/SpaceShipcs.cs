using System.Collections.Generic; // List için gerekli
using System.Drawing;             // Rectangle için gerekli
using spacewardeneme2;

namespace spacewardeneme2
{
    public class SpaceShipcs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }  // Oyuncunun sağlık seviyesi
        public int Damage { get; set; }  // Oyuncunun vereceği hasar
        public int Speed { get; set; }   // Oyuncunun hız değeri
        public int Width { get; set; } = 50;   // Genişlik
        public int Height { get; set; } = 50;  // Yükseklik

        // Çarpışma kontrolü için oyuncunun alanını döndüren özellik
        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        // Ateşlenen mermilerin listesi
        public List<Bullet> Bullets { get; private set; }

        public SpaceShipcs(int x, int y, int health = 100, int damage = 0, int speed = 0)
        {
            X = x;
            Y = y;
            Health = health;  // Başlangıç sağlık seviyesi
            Damage = damage;  // Başlangıç hasar seviyesi
            Speed = speed;    // Başlangıç hız seviyesi
            Bullets = new List<Bullet>(); // Listeyi başlat
        }

        // SpaceShip hareket ettirme metodu
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= Speed;  // Yukarı hareket
                    break;
                case Direction.Down:
                    Y += Speed;  // Aşağı hareket
                    break;
                case Direction.Left:
                    X -= Speed;  // Sola hareket
                    break;
                case Direction.Right:
                    X += Speed;  // Sağa hareket
                    break;
            }
        }

        // Oyuncu hasar aldığında sağlık seviyesini güncelleyen metod
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;  // Sağlık 0'ın altına inemez
        }

        // Oyuncunun düşmanla çatıştığında verdiği hasarı döndüren metod
        public int Attack()
        {
            return Damage;  
        }

        // Hareketi durduran metod
        public void StopMovement()
        {
            Speed = 0;  // Hız sıfırlanır hareket durur
        }

        // Mermi atma metodu
        public Bullet Shoot(Direction direction)
        {
            Bullet newBullet = new Bullet(
                this.X,          // Merminin X konumu oyuncunun X konumuyla aynı
                this.Y,          // Merminin Y konumu oyuncunun Y konumuyla aynı
                10,              // Mermi hızı
                direction,       // Sağ sol yönü için Direction enum değeri
                this.Damage      // Oyuncunun hasarı
            );

            // Mermiyi listeye ekle
            Bullets.Add(newBullet);

            return newBullet;  
        }

        // Ateşlenen mermiler listesini temizleme metodu 
        public void ClearBullets()
        {
            Bullets.Clear();
        }
    }

    // Direction enumu yönleri belirlemek için
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
