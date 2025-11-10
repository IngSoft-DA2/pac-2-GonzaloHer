import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AccessCounterService } from '../services/access-counter.service';
import { Router } from '@angular/router';

export const reflectionAccessGuard: CanActivateFn = () => {
  const counterService = inject(AccessCounterService);
  const router = inject(Router);

  const count = counterService.getCount('/reflection');

  if (count >= 20) {
    alert('¡Acceso bloqueado! Superaste el límite de accesos.');
    router.navigate(['/']);
    return false;
  }

  return true;
};
