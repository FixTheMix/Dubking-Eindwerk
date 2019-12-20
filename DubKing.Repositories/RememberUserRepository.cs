using DubKing.Model;
using DubKing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DubKing.Repositories
{
    public class RememberUserRepository : IRememberUserRepository
    {
        public void Save(User user)
        {
            if (File.Exists(@"Remembering.bin"))
            {
                File.Delete(@"Remembering.bin");
            }

            using (Stream stream = File.Open(@"Remembering.bin", FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, user);
            }
        }
        public void Delete()
        {
            if (File.Exists(@"Remembering.bin"))
            {
                File.Delete(@"Remembering.bin");
            }
        }
        public User Get()
        {
            if (!File.Exists(@"Remembering.bin"))
            {
                return new User();
            }
            else
            {
                try
                {
                    using (Stream stream = File.Open(@"Remembering.bin", FileMode.Open))
                    {
                        BinaryFormatter bin = new BinaryFormatter();

                        return (User)bin.Deserialize(stream);
                    }
                }
                catch (IOException)
                {
                    return null;
                }
            }
        }

        
    }
}


