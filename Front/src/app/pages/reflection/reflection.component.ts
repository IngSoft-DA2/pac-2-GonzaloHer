import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReflectionService } from '../../services/reflection.service';
import { AccessCounterService } from '../../services/access-counter.service';

@Component({
  selector: 'app-reflection',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reflection.component.html',
  styleUrls: ['./reflection.component.css'],
  providers: [ReflectionService]
})
export class ReflectionComponent implements OnInit {
  dllNames: string[] = [];
  isLoading = true;
  hasError = false;

  constructor(
    private reflectionService: ReflectionService,
    private counter: AccessCounterService
  ) {}

  ngOnInit(): void {
    this.counter.increment('/reflection');

    this.reflectionService.getImporterNames().subscribe({
      next: (data) => {
        this.dllNames = data;
        this.isLoading = false;
      },
      error: () => {
        this.hasError = true;
        this.isLoading = false;
      },
    });
  }
}
