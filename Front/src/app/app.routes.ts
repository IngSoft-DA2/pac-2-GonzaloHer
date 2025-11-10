import { Routes } from '@angular/router';
import { reflectionAccessGuard } from './guards/reflection-access.guard';

export const routes: Routes = [
  {
    path: 'reflection',
    loadComponent: () =>
      import('./pages/reflection/reflection.component').then(
        (m) => m.ReflectionComponent
      ),
    canActivate: [reflectionAccessGuard],
  },
  { path: '', redirectTo: '/reflection', pathMatch: 'full' },
];
