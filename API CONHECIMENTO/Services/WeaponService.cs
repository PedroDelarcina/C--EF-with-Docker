using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore;

public class WeaponService
{
    private readonly WeaponRepository _repository;

    public WeaponService(WeaponRepository repository)
    {
        _repository = repository;
    }


}

