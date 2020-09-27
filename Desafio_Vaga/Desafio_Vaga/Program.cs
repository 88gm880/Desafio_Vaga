using System;

namespace Desafio_Vaga
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Robo robo = new Robo(0, 0, 'O');
            robo.achaCaminho(4, 0);
        }

    }

    class Robo
    {
        private char[] direcoes = { 'N', 'L', 'S', 'O' };
        private int direcao;
        private int x, y;
        private String caminho = "";

        public Robo(int x, int y, char direcao)
        {
            direcao = char.ToUpper(direcao); // garante que vai receber um caracter maiusculo
            this.x = x;
            this.y = y;
            this.direcao = Array.IndexOf(direcoes, direcao);
            if (this.direcao == -1)
            {
                Console.WriteLine("Direcao invalida");
                Environment.Exit(2);
            }
            
        }

        //retorna x
        public int getX()
        {
            return this.x;
        }

        //retorna y
        public int getY()
        {
            return this.y;
        }

        public void achaCaminho(int x, int y)
        {
            int i, direcao;
            while(this.x != x || this.y != y)
            {
                if(this.x == x)
                {
                    
                }
                else if(this.x < x)
                {
                    direcao = Array.IndexOf(direcoes, 'L');
                    if (this.direcao > direcao)
                        for (i = 0; i <= this.direcao - direcao; i++)
                            virarEsq();
                    else
                        for (i = 0; i < direcao - this.direcao; i++)
                            virarDir();
                    for (i = this.x; i < x; i++)
                        movimento();
                }
                /*else
                {
                    direcao = Array.IndexOf(direcoes, 'L');
                    if (this.direcao ecao)
                        for (i = 0; i <= this.direcao - direcao; i++)
                            virarEsq();
                    else
                        for (i = 0; i < direcao - this.direcao; i++)
                            virarDir();
                    for (i = x; i < this.x; i++)
                        movimento();
                }*/

            }
            irrigar();
            Console.WriteLine(this.x +" "+ this.y);
            Console.WriteLine(caminho);
            Console.WriteLine(direcoes[this.direcao]);
        }

        //Movimenta o robo para frente na direção que ele está virado
        public void movimento()
        {
            switch (direcoes[direcao])
            {
                case 'N':
                    y++;
                    break;
                case 'S':
                    y--;
                    break;
                case 'L':
                    x++;
                    break;
                case 'O':
                    x--;
                    break;
            }
            caminho += " M";
        }

        //Vira o robô para direita
        public void virarDir()
        {
            direcao = (++direcao) % 4;
            caminho += " D";
        }

        //Vira o robô para esquerda
        public void virarEsq()
        {
           direcao = (direcao + 3) % 4;
            caminho += " E";
        }

        public void irrigar()
        {
            caminho += " I";
        }
    }
}
