using System.Windows.Forms;

namespace spacewardeneme2
{
    public class PowerUp
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PictureBox PictureBox { get; set; }
        public int Speed { get; set; }
        


        // Yapıcı metot
        public PowerUp(int x, int y,int speed)
        {
            X = x;
            Y = y;
            Speed = speed;

            // PictureBox nesnesini burada başlatıyoruz
            PictureBox = new PictureBox();
        }
            
    }
}